
using ECommerce.Api.Application.Repositories.CustomerRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Repositories.CustomerRepositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
