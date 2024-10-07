
using ECommerce.Api.Application.Abstraction.Storage;
using ECommerce.Api.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Api.Application.Features.Products.Queries.GetAllProduct;
using ECommerce.Api.Application.Repositories.FileRepositories;
using ECommerce.Api.Application.Repositories.InvoinceRepositories;
using ECommerce.Api.Application.Repositories.ProductImageFileRepositories;
using ECommerce.Api.Application.Repositories.ProductRepositories;
using ECommerce.Api.Application.RequestParameters;
using ECommerce.Api.Application.ViewModels.ProductViewModels;
using ECommerce.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoinceFileWriteRepository _invoinceFileWriteRepository;
        private readonly IInvoinceFileReadRepository _invoinceFileReadRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoinceFileWriteRepository invoinceFileWriteRepository, IInvoinceFileReadRepository invoinceFileReadRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoinceFileWriteRepository = invoinceFileWriteRepository;
            _invoinceFileReadRepository = invoinceFileReadRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {

            GetAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductViewModel model)
        {
            var product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;

            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string path)> datas = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.path,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return Ok();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                imageId = p.Id,
            }));
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {

            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImageFile? productImageFile = product.ProductImageFiles
                .FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();

            return Ok();
        }
    }
}
