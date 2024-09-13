using ECommerce.Api.Application.Repositories.ProductImageFileRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Repositories.ProductImageRepositories
{
    public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
