using Marketplace.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Downloads { get; set; }
        public string Version { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public MarketplaceUser Author { get; set; }
        public int LicenseId { get; set; }
        [ForeignKey("LicenseId")]
        public License License { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<TagItem> ItemTags { get; set; }
    }
}
