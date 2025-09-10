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
    public class ProductTierPricingRepository: IProductTierPricingRepository
    {

        private readonly AppDbContext _context;

        public ProductTierPricingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProductTierPricing> GetBestPricingAsync(int productId, int quantity)
        {
            return await _context.ProductTierPricings
                .Where(pt => pt.ProductId == productId && pt.IsApplicable(quantity))
                .OrderBy(pt => pt.MinQuantity)  // Assuming you want the most applicable tier for quantity
                .FirstOrDefaultAsync();
        }
    }
}
