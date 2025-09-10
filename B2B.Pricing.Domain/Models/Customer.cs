using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactDetails { get; set; }

        // Relationships
        public int? CustomerGroupId { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
        public List<CustomerContractPricing> Contracts { get; set; } = new List<CustomerContractPricing>();
    }


}
