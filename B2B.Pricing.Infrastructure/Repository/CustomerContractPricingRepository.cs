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
    public class CustomerContractPricingRepository : ICustomerContractPricingRepository
    {

        private readonly AppDbContext _context;

        public CustomerContractPricingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerContractPricing> GetActiveContractAsync(int customerId, int productId, DateTime now)
        {
            return await _context.CustomerContractPricings
                .Where(c => c.CustomerId == customerId && c.ProductId == productId && c.ValidFrom <= now && c.ValidTo >= now)
                .FirstOrDefaultAsync();
        }


    }
}
