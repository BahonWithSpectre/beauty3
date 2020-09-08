using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using beauty3.Models;
using beauty3.DbFolder;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace beauty3.Controllers
{
    public class HomeController : Controller
    {
        private AppDb db;

        public HomeController(AppDb _db)
        {
            db = _db;
        }



        public IActionResult Visitor()
        {
            var kurs = db.Kurs.Take(3);

            return View(kurs);
        }



        public async Task<IActionResult> Kurs(int? Id)
        {
            if(Id != null)
            {
                var kurs = await db.Kurs.FirstOrDefaultAsync(p => p.Id == Id);
                ViewBag.Videos = await db.KursVideos.Where(x => x.KursId == Id).ToListAsync();

                return View(kurs);
            }
            return View();
        }



        public async Task<IActionResult> AllKurs()
        {
            var kurs = await db.Kurs.ToListAsync();

            return View(kurs);
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
