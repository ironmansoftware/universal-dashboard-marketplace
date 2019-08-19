using Marketplace.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data
{
    public class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public MarketplaceUser Author { get; set; }
    }
}
