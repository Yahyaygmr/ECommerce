using ECommerce.Api.Application.Features.ProductImageFiles.Commands.RemoveProductImage;
using ECommerce.Api.Application.Features.ProductImageFiles.Commands.UploadProductImage;
using ECommerce.Api.Application.Features.ProductImageFiles.Queries.GetProductImages;
using ECommerce.Api.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Api.Application.Features.Products.Commands.RemoveProduct;
using ECommerce.Api.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Api.Application.Features.Products.Queries.GetAllProduct;
using ECommerce.Api.Application.Features.Products.Queries.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {

            GetAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery, FromBody] UploadProductImageCommandRequest request)
        {
            UploadProductImageCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest request)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            RemoveProductImageCommandRequest request = new() { Id = id, ImageId = imageId };
            RemoveProductImageCommandResponse response = await _mediator.Send(request);
            return Ok();
        }
    }
}
