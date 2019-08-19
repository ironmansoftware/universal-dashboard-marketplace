using System.Collections.Generic;

namespace Marketplace.Data
{
    public class NuGetProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<ModuleInfo> Packages { get; set; }
    }
}
