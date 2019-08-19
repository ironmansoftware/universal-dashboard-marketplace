using System.Collections.Generic;

namespace Marketplace.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TagItem> TagItems { get; set; }
    }
}
