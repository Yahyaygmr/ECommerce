
using ECommerce.Api.Application.Repositories.ProductRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var products =  _productReadRepository.GetAll(false);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product =await _productReadRepository.GetByIdAsync(id,false);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new(){Id = Guid.NewGuid(),Name="Product 1", Price = 100, CreatedDate = DateTime.UtcNow, Stock = 60},
            //    new(){Id = Guid.NewGuid(),Name="Product 2", Price = 200, CreatedDate = DateTime.UtcNow, Stock = 50},
            //    new(){Id = Guid.NewGuid(),Name="Product 3", Price = 300, CreatedDate = DateTime.UtcNow, Stock = 40},
            //    new(){Id = Guid.NewGuid(),Name="Product 4", Price = 400, CreatedDate = DateTime.UtcNow, Stock = 30},
            //    new(){Id = Guid.NewGuid(),Name="Product 5", Price = 500, CreatedDate = DateTime.UtcNow, Stock = 20},
            //    new(){Id = Guid.NewGuid(),Name="Product 6", Price = 600, CreatedDate = DateTime.UtcNow, Stock = 10},
            //});
            //await _productWriteRepository.SaveAsync();

            await _productWriteRepository.AddAsync(new() { Name = "Product new", Price = 300, Stock = 60 });
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(string id)
        {
            var product =await _productReadRepository.GetByIdAsync(id);
            product.Name = "Güncellenmiş İsim";
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
