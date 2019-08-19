using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data
{
    public class TagItem
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}
