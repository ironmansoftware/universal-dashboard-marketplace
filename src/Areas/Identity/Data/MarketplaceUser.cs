using Marketplace.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Marketplace.Areas.Identity.Data
{
    public class MarketplaceUser : IdentityUser
    {
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
