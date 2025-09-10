using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Domain.Models
{
    public class CustomerGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relationships
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<CustomerGroupPricing> Pricing { get; set; } = new List<CustomerGroupPricing>();
    }


}
