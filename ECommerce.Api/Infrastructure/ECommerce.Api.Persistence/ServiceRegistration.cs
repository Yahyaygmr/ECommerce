using ECommerce.Api.Application.Abstraction;
using ECommerce.Api.Persistence.Concretes;
using ECommerce.Api.Persistence.Configurations;
using ECommerce.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Persistence
{
    public static class ServiceRegistration
    {
        public static void PersistenceServiceRegistrations(this IServiceCollection services)
        {
            services.AddDbContext<ECommerceApiDbContext>(options =>
            {
                options.UseNpgsql(Configuration.ConnectionString);
            });
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
