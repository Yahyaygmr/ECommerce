using ECommerce.Api.Application.Repositories.ProductRepositories;
using MediatR;

namespace ECommerce.Api.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandle : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandle(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new ()
            {
                Name= product.Name,
                Price= product.Price,
                Stock= product.Stock,
                Orders= product.Orders,
                ProductImageFiles = product.ProductImageFiles,
            };
        }
    }
}
