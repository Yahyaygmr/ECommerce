using ECommerce.Api.Application.Repositories.ProductRepositories;
using MediatR;

namespace ECommerce.Api.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandle : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        public Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
        }
    }
}
