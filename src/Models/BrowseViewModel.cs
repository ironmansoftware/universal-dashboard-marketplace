using Marketplace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Models
{
    public class BrowseViewModel
    {
        public IEnumerable<ModuleInfo> Modules { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Type { get; set; }
        public string OrderBy { get; set; }
        public int TotalItems { get; set; }
    }
}
