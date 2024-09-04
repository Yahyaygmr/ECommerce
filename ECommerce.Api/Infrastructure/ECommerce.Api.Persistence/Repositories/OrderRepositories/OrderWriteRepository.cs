
using ECommerce.Api.Application.Repositories.OrderRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Repositories.OrderRepositories
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
