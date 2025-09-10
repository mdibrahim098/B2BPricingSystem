using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Domain.Models
{
    public class CustomerGroupPricing
    {
        public int Id { get; set; }
        public int CustomerGroupId { get; set; }
        public int ProductId { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal GroupPrice { get; set; }

        // Relationships
        public CustomerGroup CustomerGroup { get; set; }
        public Product Product { get; set; }

        // Checks if the group pricing is applicable for a specific quantity
        public bool IsApplicable(int quantity)
        {
            return quantity >= MinQuantity && quantity <= MaxQuantity;
        }


    }
}
