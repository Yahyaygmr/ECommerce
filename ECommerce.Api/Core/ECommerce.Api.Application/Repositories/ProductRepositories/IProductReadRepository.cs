﻿using ECommerce.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Repositories.ProductRepositories
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
    }
}
