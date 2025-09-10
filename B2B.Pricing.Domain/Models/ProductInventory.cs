using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Domain.Models
{
    public class ProductInventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityAvailable { get; set; }

        // Relationship
        public Product Product { get; set; }


    }
}
