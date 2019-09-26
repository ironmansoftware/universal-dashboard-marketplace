using Marketplace.Data;
using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Controllers
{
    public class BrowseController : Controller
    {
        private ApplicationDbContext _dbContext;

        public BrowseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index([FromQuery]int page = 1, [FromQuery]string type = "any", [FromQuery]string orderBy = "downloads")
        {
            const int pageSize = 9;

            if (page < 1)
            {
                page = 1;
            }
            
            var query = _dbContext.ModuleInfo.Where(x => x != null);
            if (Enum.TryParse(type, out ItemType itemType))
            {
                query = query.Where(m => m.Type == itemType);
            }

            var total = query.Count();

            switch(orderBy)
            {
                case "downloads":
                    query = query.OrderByDescending(m => m.DownloadCount);
                    break;
                case "recent":
                    query = query.OrderByDescending(m => m.Published);
                    break;
                default:
                    query = query.OrderByDescending(m => m.DownloadCount);
                    break;
            }

            var items = query.Skip((page - 1) * pageSize).Take(pageSize);

            return View(new BrowseViewModel
            {
                TotalPages = total / pageSize,
                TotalItems = total,
                CurrentPage = page,
                Modules = items,
                Type = type, 
                OrderBy = orderBy
            });
        }
    }
}
