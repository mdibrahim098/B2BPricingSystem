using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2B.Pricing.Domain.Models;

namespace B2B.Pricing.Application.Services.Interfaces
{
    public interface ICustomerGroupPricingRepository
    {

        Task<CustomerGroupPricing> GetBestPricingAsync(int customerId, int productId, int quantity);

    }
}
