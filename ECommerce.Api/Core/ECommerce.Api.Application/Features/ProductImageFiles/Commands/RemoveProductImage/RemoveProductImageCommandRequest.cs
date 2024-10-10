using MediatR;

namespace ECommerce.Api.Application.Features.ProductImageFiles.Commands.RemoveProductImage
{
    public class RemoveProductImageCommandRequest : IRequest<RemoveProductImageCommandResponse>
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
    }
}
