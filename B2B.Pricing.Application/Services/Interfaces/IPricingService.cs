using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B.Pricing.Application.Services.Interfaces
{
    public interface IPricingService
    {
        Task<decimal> ResolvePriceAsync(int customerId, int productId, int quantity, DateTime now);

    }
}
