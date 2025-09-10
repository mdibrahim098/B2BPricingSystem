
using B2B.Pricing.Application.Services.Interfaces;
using B2B.Pricing.Domain.Models;
using B2B.Pricing.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace B2B.Pricing.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            //return await _context.Products
            //    .Include(p => p.TierPricing)
            //    .FirstOrDefaultAsync(p => p.Id == productId);

            return await _context.Products
           .Include(p => p.TierPricing)  // Include any related data
           .Include(p => p.Inventory)
           .Include(p => p.CustomerGroupPricing)
           .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //return await _context.Products.ToListAsync();
            return await _context.Products
           .Include(p => p.TierPricing)  // Include related data
           .Include(p => p.Inventory)
           .Include(p => p.CustomerGroupPricing)
           .Take(5) // Limit the number of products for minimal view
           .ToListAsync();
        }


    }
}
