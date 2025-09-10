using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2B.Pricing.Domain.Models;

namespace B2B.Pricing.Application.Services.Interfaces
{
    public interface IProductRepository
    {

        Task<Product> GetByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
