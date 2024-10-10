using ECommerce.Api.Application.Repositories.ProductRepositories;
using ECommerce.Api.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Api.Application.Features.ProductImageFiles.Queries.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IConfiguration _configuration;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                 .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return product!.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                FileName = p.FileName,
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                ImageId = p.Id.ToString(),
            }).ToList();
        }
    }
}