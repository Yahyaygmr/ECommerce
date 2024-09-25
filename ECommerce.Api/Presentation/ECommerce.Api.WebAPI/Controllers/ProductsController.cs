
using ECommerce.Api.Application.Abstraction.Storage;
using ECommerce.Api.Application.Repositories.FileRepositories;
using ECommerce.Api.Application.Repositories.InvoinceRepositories;
using ECommerce.Api.Application.Repositories.ProductImageFileRepositories;
using ECommerce.Api.Application.Repositories.ProductRepositories;
using ECommerce.Api.Application.RequestParameters;
using ECommerce.Api.Application.ViewModels.ProductViewModels;
using ECommerce.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileReadRepository fileReadRepository, IFileWriteRepository fileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoinceFileWriteRepository invoinceFileWriteRepository, IInvoinceFileReadRepository invoinceFileReadRepository, IStorageService storageService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
            });
            return Ok(new
            {
                totalCount,
                products,
                pagination.Page,
                pagination.Size,
            });
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductViewModel model)
        {
            Product product = new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            };
            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
        {
            var datas = await _storageService.UploadAsync("resource/files", Request.Form.Files);
            //var datas= await _fileService.UploadAsync("resource/files", Request.Form.Files);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile
            {
                FileName = d.filename,
                Path = d.path,
                Storage = _storageService.StorageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            //await _invoinceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoinceFile
            //{
            //    FileName = d.filename,
            //    Path = d.path,
            //    Price = new Random().Next()
            //}).ToList());
            //await _invoinceFileWriteRepository.SaveAsync();
            //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new CFile
            //{
            //    FileName = d.filename,
            //    Path = d.path,
            //    Storage = _storageService.StorageName,
            //}).ToList());
            //await _fileWriteRepository.SaveAsync();

            return Ok(new { message = "Files successfully uploaded" });
        }
    }
}
