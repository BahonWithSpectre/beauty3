using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using beauty3.DbFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beauty3.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        public readonly AppDb db;
        public AdminController(AppDb _db)
        {
            db = _db;
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

        public IActionResult KursEdit()
        {
            return View();
        }

        public IActionResult DeleteKurs()
        {
            return RedirectToAction("Index");
        }



    }
}