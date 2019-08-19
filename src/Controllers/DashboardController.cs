using Marketplace.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Marketplace.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DashboardController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet("/dashboard/{nugetId}")]
        public IActionResult Index([FromRoute]string nugetId)
        {
            var item = applicationDbContext.ModuleInfo.FirstOrDefault(m => m.NuGetId == nugetId);

            if (item == null) return NotFound();

            return View(item);
        }
    }
}
