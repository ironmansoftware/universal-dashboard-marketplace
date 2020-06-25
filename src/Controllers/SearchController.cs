﻿using Marketplace.Data;
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

        private SearchViewModel GetResults(string searchText, int page)
        {
            const int pageSize = 9;
            var text = searchText;

            var total = _dbContext.ModuleInfo.Where(m => m.Title.Contains(text) || m.Readme.Contains(text) || m.Description.Contains(text) || m.Tags.Contains(text) || m.Authors.Contains(text)).Count();
            var items = _dbContext.ModuleInfo.Where(m => m.Title.Contains(text) || m.Readme.Contains(text) || m.Description.Contains(text) || m.Tags.Contains(text) || m.Authors.Contains(text)).OrderByDescending(m => m.DownloadCount); //.Skip(page * pageSize).Take(pageSize);

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
        public IActionResult JsonResult([FromForm]string searchText, [FromQuery]int page)
        {
            return Json(GetResults(searchText, page));
        }

        [HttpPost("/search/result")]
        public IActionResult Result([FromForm]string searchText, [FromQuery]int page)
        {
            return View(GetResults(searchText, page));
        }
    }
}
