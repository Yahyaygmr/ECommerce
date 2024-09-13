using ECommerce.Api.Application.Repositories.FileRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Repositories.FileRepositories
{
    public class FileReadRepository : ReadRepository<CFile>, IFileReadRepository
    {
        public FileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
