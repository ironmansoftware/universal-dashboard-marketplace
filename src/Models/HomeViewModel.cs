using Marketplace.Data;
using System.Collections.Generic;

namespace Marketplace.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ModuleInfo> NewPackages { get; set; }
        public IEnumerable<ModuleInfo> MostDownloadedPackages { get; set; }
        public IEnumerable<Statistics> LastThirdyDaysStatistics { get; set; }
    }
}
