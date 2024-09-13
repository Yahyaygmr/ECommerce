using ECommerce.Api.Application.Repositories.FileRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;

namespace ECommerce.Api.Persistence.Repositories.FileRepositories
{
    public class FileWriteRepository : WriteRepository<CFile>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
