﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using beauty3.DbFolder;
using beauty3.Models;
using beauty3.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace beauty3.Controllers
{
    public class AccountController : Controller
    {
        AppDb db;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        private IHttpContextAccessor _accessor;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppDb _db, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = _db;
            _accessor = accessor;
        }

        // Register Register Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.PhoneNumber = model.PhoneNumber.Replace("+", "")
                .Replace("(", "").Replace(")", "").Replace(" ", "");
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.PhoneNumber, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Profil", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        // Login Login Login

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("UserList", "Admin");
                }
                else
                {
                    return RedirectToAction("Profil", "Account");
                }
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            model.PhoneNumber = model.PhoneNumber.Replace("+", "")
                .Replace("(", "").Replace(")", "").Replace(" ", "");

            if (ModelState.IsValid)
            {
                var us = await _userManager.FindByNameAsync(model.PhoneNumber);
                if(us.Stats == true)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {

                        //////////////////// IP Adress ///////////////////////
                        var ipp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                        await db.UserIpLists.AddAsync(new UserIpList { UserId = us.Id, Ip = ipp });
                        await db.SaveChangesAsync();

                        //////////////////////////////////////////////////////

                        // проверяем, принадлежит ли URL приложению
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Profil", "Account");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Құпиясөз және(немесе) логин қате!");
                    }
                }
                else
                {
                    ViewBag.Stats = "Вы нарушаете правило! В один аккаунт может войти только валделец этого аккаунта";
                }

            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



        // Profil Profil Profil

        [Authorize]
        public async Task<IActionResult> Profil()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("UserList", "Admin");
                }
                else
                {
                    var uk = await db.UserKurs.Where(x => x.UserId == user.Id).ToListAsync();
                    ViewBag.kurses = await db.Kurs.ToListAsync();
                    return View(uk);
                }
            }
            return RedirectToAction("Login");
        }



        [Authorize]
        public async Task<IActionResult> Video(int id, int videoId = 0)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("UserList", "Admin");
                }
                else
                {
                    var kurs = await db.Kurs.FirstOrDefaultAsync(x => x.Id == id);
                    KursVideo video;
                    List<KursVideo> otherVideos;
                    if (videoId == 0)
                    {
                        video = await db.KursVideos.FirstOrDefaultAsync(x => x.KursId == kurs.Id);
                        otherVideos = await db.KursVideos.Where(x => x.Id != video.Id && x.KursId == id).Include(p => p.Kurs).ToListAsync();
                    }
                    else
                    {
                        video = await db.KursVideos.FirstOrDefaultAsync(x => x.Id == videoId && x.KursId == kurs.Id);
                        otherVideos = await db.KursVideos.Where(x => x.Id != videoId && x.KursId == id).Include(p => p.Kurs).ToListAsync();
                    }
                    ViewBag.OtherVideos = otherVideos;

                    return View(video);
                }
            }
            return RedirectToAction("Login");
        }

        // ForgotPassword ForgotPassword ForgotPassword

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // пользователь с данным email может отсутствовать в бд
                    return View("Login");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);

                EmailService emailService = new EmailService();

                try
                {
                    await emailService.SendEmailAsync(model.Email, "Reset Password",
                        $"Құпиясөзді ауыстыру үшін сілтемеге өтіңіз: <a style=\"font-size: 18px;\" href='{callbackUrl}'>Құпиясөз ауыстыру</a>");
                    return View("ForgotPasswordConfirm");
                }
                catch (Exception err)
                {
                    return Content("err: " + err);
                }

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ForgotPasswordConfirm()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("ForgotPassword") : View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.PhoneNumber = model.PhoneNumber
                .Replace("+", "").Replace(" ", "").Replace("(", "")
                .Replace(")", "");
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            if (user == null)
            {
                return View("Login");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirm");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }




        public IActionResult News()
        {
            return View();
        }



        [HttpPost]
        public async Task<JsonResult> UserOpen([FromBody] UserStatsClass mod)
        {
            mod.Id = mod.Id.Replace("+", "")
                .Replace("(", "").Replace(")", "").Replace(" ", "");

            var user = await _userManager.FindByNameAsync(mod.Id);
            if(user != null)
            {
                user.Stats = true;
                await _userManager.UpdateAsync(user);
                await db.SaveChangesAsync();

                return new JsonResult(mod.Id);
            }
            return new JsonResult(null);
        }


        [HttpPost]
        public async Task<JsonResult> UserClose([FromBody] UserStatsClass mod)
        {
            mod.Id = mod.Id.Replace("+", "")
                .Replace("(", "").Replace(")", "").Replace(" ", "");

            var user = await _userManager.FindByNameAsync(mod.Id);
            if (user != null)
            {
                user.Stats = false;
                await _userManager.UpdateAsync(user);
                await db.SaveChangesAsync();
            }
            return new JsonResult("false");
        }



        [HttpPost]
        public async Task<JsonResult> NumberRegix([FromBody] UserStatsClass mod)
        {
            mod.Id =  mod.Id.Replace("+", "")
                .Replace("(", "").Replace(")", "").Replace(" ", "");

            return new JsonResult(mod.Id);
        }

    }
}