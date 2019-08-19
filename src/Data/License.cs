using System.Collections.Generic;

namespace Marketplace.Data
{
    public class License
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool Custom { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
