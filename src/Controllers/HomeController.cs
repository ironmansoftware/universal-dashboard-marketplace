using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Models;
using Marketplace.Data;
using System.Linq;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(GetResults());
        }

        [Route("/api/overview")]
        public IActionResult Json()
        {
            return Json(GetResults());
        }

        private HomeViewModel GetResults()
        {
            return new HomeViewModel
            {
                NewPackages = _dbContext.ModuleInfo.OrderByDescending(m => m.Published).Take(6),
                MostDownloadedPackages = _dbContext.ModuleInfo.OrderByDescending(m => m.DownloadCount).Take(6),
                LastThirdyDaysStatistics = _dbContext.Statistics.OrderByDescending(m => m.Timestamp).Take(30)
            };
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
