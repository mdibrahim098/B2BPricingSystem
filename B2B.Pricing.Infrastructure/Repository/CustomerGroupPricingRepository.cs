using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2B.Pricing.Application.Services.Interfaces;
using B2B.Pricing.Domain.Models;
using B2B.Pricing.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace B2B.Pricing.Infrastructure.Repository
{
    public class CustomerGroupPricingRepository : ICustomerGroupPricingRepository
    {

        private readonly AppDbContext _context;

        public CustomerGroupPricingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerGroupPricing> GetBestPricingAsync(int customerId, int productId, int quantity)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerGroup)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer?.CustomerGroup != null)
            {
                // Get all relevant group pricing for the product and customer group
                var groupPricings = await _context.CustomerGroupPricings
                    .Where(cgp => cgp.ProductId == productId && cgp.CustomerGroupId == customer.CustomerGroupId)
                    .ToListAsync();  // Fetch data in memory

                // Now, filter in-memory using IsApplicable
                var applicablePricing = groupPricings
                    .Where(cgp => cgp.IsApplicable(quantity))  // Filtering in memory
                    .OrderBy(cgp => cgp.MinQuantity)  // Choose the best pricing tier
                    .FirstOrDefault();


                return applicablePricing;
            }

            return null;
        }

    }
}
