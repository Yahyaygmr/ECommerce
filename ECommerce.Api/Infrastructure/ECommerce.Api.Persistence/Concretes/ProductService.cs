using ECommerce.Api.Application.Abstraction;
using ECommerce.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetAllProducts()
            => new List<Product>
            {
                new Product(){Id=Guid.NewGuid(),Name="Product1",Price=254,Stock=10,CreatedDate=DateTime.UtcNow},
                new Product(){Id=Guid.NewGuid(),Name="Product2",Price=145,Stock=10,CreatedDate=DateTime.UtcNow},
                new Product(){Id=Guid.NewGuid(),Name="Product3",Price=700,Stock=140,CreatedDate=DateTime.UtcNow},
                new Product(){Id=Guid.NewGuid(),Name="Product4",Price=120,Stock=10,CreatedDate=DateTime.UtcNow},
                new Product(){Id=Guid.NewGuid(),Name="Product5",Price=350,Stock=140,CreatedDate=DateTime.UtcNow},
                new Product(){Id=Guid.NewGuid(),Name="Product6",Price=88,Stock=150,CreatedDate=DateTime.UtcNow},
                new Product(){Id=Guid.NewGuid(),Name="Product7",Price=25,Stock=160,CreatedDate=DateTime.UtcNow},
            };
    }
}
