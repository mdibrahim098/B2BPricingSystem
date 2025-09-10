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
    public class CustomerRepository : ICustomerRepository
    {

        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            return await _context.Customers
                .Include(c => c.CustomerGroup)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

    }
}
