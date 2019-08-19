using Marketplace.Data;
using System;
using System.Collections.Generic;

namespace Marketplace.Models
{
    public class SearchViewModel
    {
        public IEnumerable<ModuleInfo> Modules { get; set; }
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
