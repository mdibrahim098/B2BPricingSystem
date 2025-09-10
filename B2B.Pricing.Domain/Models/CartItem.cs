using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ResolvedPrice { get; set; }
        public string PriceSource { get; set; } // Contract, Group_Tier, Product_Tier, Base

        // Relationships
        public Product Product { get; set; }

    }
}
