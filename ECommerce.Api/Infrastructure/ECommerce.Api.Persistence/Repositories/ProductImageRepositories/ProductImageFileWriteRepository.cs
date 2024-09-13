using ECommerce.Api.Application.Repositories.ProductImageFileRepositories;
using ECommerce.Api.Domain.Entities;
using ECommerce.Api.Persistence.Context;

namespace ECommerce.Api.Persistence.Repositories.ProductImageRepositories
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ECommerceApiDbContext context) : base(context)
        {
        }
    }
}
