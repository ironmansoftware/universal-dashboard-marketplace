using Marketplace.Data;
using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Marketplace.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext _dbContext;

        public SearchController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private SearchViewModel GetResults(string searchText, int page, bool includePSCommander)
        {
            const int pageSize = 10;
            var text = searchText;

            var query = _dbContext.ModuleInfo.Where(m => m.Title.Contains(text) || m.Readme.Contains(text) || m.Description.Contains(text) || m.Tags.Contains(text) || m.Authors.Contains(text));
            if (!includePSCommander)
            {
                query = query.Where(m => m.Type != ItemType.PSCommander);
            }

            var total = query.Count();
            var items = query.OrderByDescending(m => m.DownloadCount).Skip(page * pageSize).Take(pageSize);

            return new SearchViewModel
            {
                TotalPages = total / pageSize,
                TotalItems = total,
                SearchTerm = text,
                CurrentPage = page,
                Modules = items
            };
        }

        [HttpPost("/api/search")]
        public IActionResult JsonResult([FromForm] string searchText, [FromQuery] int page)
        {
            return Json(GetResults(searchText, page, false));
        }

        [HttpPost("/search/result")]
        public IActionResult Result([FromForm] string searchText, [FromQuery] int page)
        {
            return View(GetResults(searchText, page, true));
        }
    }
}
