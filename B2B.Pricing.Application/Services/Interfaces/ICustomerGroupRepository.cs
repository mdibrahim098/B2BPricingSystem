using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B2B.Pricing.Domain.Models;

namespace B2B.Pricing.Application.Services.Interfaces
{
    public interface ICustomerGroupRepository
    {
        Task<CustomerGroup> GetByIdAsync(int customerGroupId);
        Task<IEnumerable<CustomerGroup>> GetAllAsync();

    }
}
