using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using beauty3.Models;
using beauty3.DbFolder;

namespace beauty3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDb db;

        public HomeController(ILogger<HomeController> logger, AppDb _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Visitor()
        {
            var kurs = db.Kurs.ToList();

            return View(kurs);
        }



        public IActionResult Kurs(int? Id)
        {
            if(Id != null)
            {
                var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);

                return View(kurs);
            }
            return View();
        }




















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
