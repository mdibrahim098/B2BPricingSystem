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
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        private readonly AppDbContext _context;

        public CustomerGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerGroup> GetByIdAsync(int customerGroupId)
        {
            return await _context.CustomerGroups
                .FirstOrDefaultAsync(cg => cg.Id == customerGroupId);
        }

        public async Task<IEnumerable<CustomerGroup>> GetAllAsync()
        {
            return await _context.CustomerGroups.ToListAsync();
        }

    }
}
