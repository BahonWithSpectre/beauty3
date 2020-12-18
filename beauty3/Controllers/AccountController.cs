using System;
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
                    await _signInManager.SignInAsync(user, true);
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
                if (us != null)
                {
                    if (us.Ban == true)
                    {
                        ViewBag.Stats = "Бұл аккаунт бұғатталған(қара тізімде). Толық ақпаратты білу үшін Администратормен байланысыңыз! +7 (700) 497 6277 (WhatsApp)";
                        return View(model);
                    }
                    //if (us.Stats == true || us.Stats == false || us.Stats == null)
                    //{
                        var result = await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, true, true);
                        if (result.Succeeded)
                        {
                            //////////////////// IP Adress ///////////////////////
                            var ipp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                            var ipreg = await db.UserIpLists.Where(p => p.User.UserName == model.PhoneNumber).FirstOrDefaultAsync();
                            if (ipreg == null)
                            {
                                await db.UserIpLists.AddAsync(new UserIpList { UserId = us.Id, Ip = ipp });
                            }
                            else
                            {
                                if (ipreg.Ip != ipp)
                                {
                                    if (ipreg.Ip2 != null && ipreg.Ip2 != ipp)
                                    {
                                        if (ipreg.Ip3 != null && ipreg.Ip3 != ipp)
                                        {
                                            if (ipreg.Ip4 != null && ipreg.Ip4 != ipp)
                                            { }
                                            else
                                            {
                                                ipreg.Ip4 = ipp;
                                            }
                                        }
                                        else
                                        {
                                            ipreg.Ip3 = ipp;
                                        }
                                    }
                                    else
                                    {
                                        ipreg.Ip2 = ipp;
                                    }
                                    db.UserIpLists.Update(ipreg);
                                }
                            }
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
                            //ModelState.AddModelError("", "Құпиясөз және(немесе) логин қате!");
                            ModelState.AddModelError("", "Құпиясөз дұрыс емес!");
                        }
                    //}
                    //else
                    //{
                    //    ViewBag.Stats = "Ережені бұзбаңыз! Дәл қазір бұл аккаунт қолданыс үстінде, аккаунтты тек иесі ғана қолданады!";
                    //}
                }
                else
                {
                    //ModelState.AddModelError("", "Бұл номер жүйеде тіркелмеген! Тіркелу үшін <a href=\"/Account/Register\">тіркелу бетіне</a> өтіңіз!");
                    ModelState.AddModelError("", "Бұл номер жүйеде тіркелмеген!");
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
                    List<KursVideo> otherVideos;
                    VideoViewModel videoView = new VideoViewModel();
                    if (videoId == 0)
                    {
                        videoView.KursVideo = await db.KursVideos.FirstOrDefaultAsync(x => x.KursId == kurs.Id);
                        otherVideos = await db.KursVideos.Where(x => x.Id != videoView.KursVideo.Id && x.KursId == id).Include(p => p.Kurs).ToListAsync();
                    }
                    else
                    {
                        videoView.KursVideo = await db.KursVideos.FirstOrDefaultAsync(x => x.Id == videoId && x.KursId == kurs.Id);
                        otherVideos = await db.KursVideos.Where(x => x.Id != videoId && x.KursId == id).Include(p => p.Kurs).ToListAsync();
                    }
                    ViewBag.OtherVideos = otherVideos;
                    var vc = await db.VideoComments.Where(x => x.KursVideoId == videoView.KursVideo.Id).Include(x => x.User).ToListAsync();

                    //videoView.Users = await db.Users.ToListAsync();
                    videoView.VideoComments = vc;
                    ViewBag.ComCount = vc.Count();

                    return View(videoView);
                }
            }
            return RedirectToAction("Login");
        }

        #region
        //[HttpPost]
        //public async Task<IActionResult> Comment(int videoId, int kursId, string userName, string text)
        //{
        //    var user = await _userManager.FindByNameAsync(userName);
        //    if(user != null)
        //    {
        //        VideoComment comment = new VideoComment()
        //        {
        //            User = user,
        //            UserId = user.Id,
        //            KursVideoId = videoId,
        //            Text = text,
        //            //DateTime = DateTime.Now.ToLocalTime()
        //        };
        //        await db.VideoComments.AddAsync(comment);
        //        await db.SaveChangesAsync();


        #endregion



        public async Task<IActionResult> CommentJS(int videoId, string userName, string dateTime, string text)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                VideoComment comment = new VideoComment()
                {
                    UserId = user.Id,
                    KursVideoId = videoId,
                    Text = text,
                    DateTime = dateTime
                };
                await db.VideoComments.AddAsync(comment);
                await db.SaveChangesAsync();

                return Json(comment);
            }
            return RedirectToAction("Profil");
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
                //var user = await _userManager.FindByEmailAsync(model.Email);
                var user = await db.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
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
        public JsonResult NumberRegix([FromBody] UserStatsClass mod)
        {
            mod.Id =  mod.Id.Replace("+", "")
                .Replace("(", "").Replace(")", "").Replace(" ", "");

            return new JsonResult(mod.Id);
        }

    }
}