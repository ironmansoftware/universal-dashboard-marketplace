using Marketplace.Areas.Identity.Data;
using System;

namespace Marketplace.Data
{
    public class ModuleInfo
    {
        public int Id { get; set; }
        public string NuGetId { get; set; }
        public string Version { get; set; }
        public string ProjectUrl { get; set;  }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Summary { get; set;  }
        public bool RequireLicenseAcceptance { get; set;  }
        public string Owners { get; set;  }
        public DateTimeOffset? Published { get; set;  }
        public string ReportAbuseUrl { get; set;  }
        public string LicenseUrl { get; set;  }
        public string IconUrl { get; set;  }
        public long? DownloadCount { get; set;  }
        public string Description { get; set;  }
        public string Authors { get; set;  }
        public string Readme { get; set; }
        public ItemType Type { get; set; }
        public MarketplaceUser SubmittedBy { get; set; }
    }
}
