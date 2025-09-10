using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Domain.Models
{
    public class CustomerContractPricing
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        // Relationships
        public Customer Customer { get; set; }
        public Product Product { get; set; }

        // Checks if the contract is valid for the given date
        public bool IsValid(DateTime date)
        {
            return date >= ValidFrom && date <= ValidTo;
        }

    }
}
