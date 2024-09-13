using ECommerce.Api.Application.Repositories.InvoinceRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Repositories.InvoinceFileRepositories
{
    public class InvoinceFileReadRepository : ReadRepository<InvoinceFile>, IInvoinceFileReadRepository
    {
        public InvoinceFileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
