using ECommerce.Api.Application.Repositories.InvoinceRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;

namespace ECommerce.Api.Persistence.Repositories.InvoinceFileRepositories
{
    public class InvoinceFileWriteRepository : WriteRepository<InvoinceFile>, IInvoinceFileWriteRepository
    {
        public InvoinceFileWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
