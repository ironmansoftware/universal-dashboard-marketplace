using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
