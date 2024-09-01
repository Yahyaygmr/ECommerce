using ECommerce.Api.Application.Abstraction;
using ECommerce.Api.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Persistence
{
    public static class ServiceRegistration
    {
        public static void PersistenceServiceRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
