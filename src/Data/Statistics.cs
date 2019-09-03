using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Data
{
    public class Statistics
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public long TotalModules { get; set; }
        public long TotalControls { get; set; }
        public long TotalDashboards { get; set; }
        public long TotalDownloads { get; set; }
    }
}
