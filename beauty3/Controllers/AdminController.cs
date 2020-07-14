using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using beauty3.DbFolder;
using beauty3.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace beauty3.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        public readonly AppDb db;
        public IWebHostEnvironment env;


        public AdminController(AppDb _db, IWebHostEnvironment _env)
        {
            db = _db;
            env = _env;
        }

        public IActionResult Index()
        {
            var kurs = db.Kurs.ToList();

            return View(kurs);
        }




        public IActionResult KursCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KursCreate(Kurs kurs, IFormFile file)
        {
            if (file == null || file.Length == 0) return Content("file not found");

            var imgname = DateTime.Now.ToString("MMddHHmmss") + file.FileName;
            string path_Root = env.WebRootPath;

            string path_to_Images = path_Root + "\\kurs\\" + imgname;
            using (var stream = new FileStream(path_to_Images, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            kurs.PhotoUrl = "/kurs/" + imgname;
            kurs.CreatDate = DateTime.Now;

            db.Kurs.Add(kurs);
            db.SaveChanges();

            return RedirectToAction("Index","Admin");
        }




        public IActionResult KursEdit(int Id)
        {
            var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);



            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> KursEdit(Kurs kurs, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                
            }
            else
            {
                var imgname = DateTime.Now.ToString("MMddHHmmss") + file.FileName;
                string path_Root = env.WebRootPath;

                string path_to_Images = path_Root + "\\kurs\\" + imgname;
                using (var stream = new FileStream(path_to_Images, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                kurs.PhotoUrl = "/kurs/" + imgname;
            }

            db.Kurs.Update(kurs);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Admin");
            
        }




        public IActionResult DeleteKurs(int Id)
        {
            var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);


            return View(kurs);
        }
        [HttpPost]
        public IActionResult DeleteKurs(Kurs kurs)
        {
            db.Kurs.Remove(kurs);

            db.SaveChanges();

            return RedirectToAction("Index","Admin");
        }


        public IActionResult InKurs(int Id)
        {
            var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);
            var count = db.UserKurs.Where(p => p.KursId == Id).ToList().Count;
            InKursView ikv = new InKursView { Kurs = kurs, KursBuy = count  };
            return View(ikv);
        }



        [HttpPost]
        public async Task<IActionResult> AddVideo(string VideoName, string Info, IFormFile PhotoUrl, string VideoUrl, int KursId)
        {
            KursVideo kv = new KursVideo { VideoName = VideoName, Info = Info, KursId = KursId, VideoUrl = VideoUrl };

            if (PhotoUrl == null || PhotoUrl.Length == 0)
            {

            }
            else
            {
                var imgname = DateTime.Now.ToString("MMddHHmmss") + PhotoUrl.FileName;
                string path_Root = env.WebRootPath;

                string path_to_Images = path_Root + "\\kurs\\" + imgname;
                using (var stream = new FileStream(path_to_Images, FileMode.Create))
                {
                    await PhotoUrl.CopyToAsync(stream);
                }

                kv.PhotoUrl = "/kurs/" + imgname;
            }

            db.KursVideos.Add(kv);
            await db.SaveChangesAsync();

            return RedirectToAction("InKurs", new { Id = kv.KursId });
        }




        public IActionResult EditVideo(int Id)
        {
            var video = db.KursVideos.FirstOrDefault(p => p.Id == Id);

            return View(video);
        }
        [HttpPost]
        public async Task<IActionResult> EditVideo(KursVideo video, IFormFile file)
        {
            if (file == null || file.Length == 0) return Content("file not found");
            else
            {
                var imgname = DateTime.Now.ToString("MMddHHmmss") + file.FileName;
                string path_Root = env.WebRootPath;

                string path_to_Images = path_Root + "\\kursvideo\\" + imgname;
                using (var stream = new FileStream(path_to_Images, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                video.PhotoUrl = "/kursvideo/" + imgname;
            }

            db.KursVideos.Update(video);

            db.SaveChanges();

            return RedirectToAction("InKurs", new { Id = video.KursId });

        }


        public IActionResult DeleteVideo(int Id)
        {
            var video = db.KursVideos.FirstOrDefault(p => p.Id == Id);

            db.KursVideos.Remove(video);
            db.SaveChanges();

            return RedirectToAction("InKurs", new { Id = video.KursId });
        }


    }
}