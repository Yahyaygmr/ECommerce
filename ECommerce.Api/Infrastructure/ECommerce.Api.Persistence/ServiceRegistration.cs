
using ECommerce.Api.Application.Repositories.CustomerRepositories;
using ECommerce.Api.Application.Repositories.FileRepositories;
using ECommerce.Api.Application.Repositories.InvoinceRepositories;
using ECommerce.Api.Application.Repositories.OrderRepositories;
using ECommerce.Api.Application.Repositories.ProductImageFileRepositories;
using ECommerce.Api.Application.Repositories.ProductRepositories;
using ECommerce.Api.Persistence.Configurations;
using ECommerce.Api.Persistence.Context;
using ECommerce.Api.Persistence.Repositories.CustomerRepositories;
using ECommerce.Api.Persistence.Repositories.FileRepositories;
using ECommerce.Api.Persistence.Repositories.InvoinceFileRepositories;
using ECommerce.Api.Persistence.Repositories.OrderRepositories;
using ECommerce.Api.Persistence.Repositories.ProductImageRepositories;
using ECommerce.Api.Persistence.Repositories.ProductRepositories;
using ECommerce.Api.Persistence.Repositories.Repositories.OrderRepositories;
using ETicaretAPI.Persistence.Repositories.ProductRepositories;
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

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IInvoinceFileReadRepository, InvoinceFileReadRepository>();
            services.AddScoped<IInvoinceFileWriteRepository, InvoinceFileWriteRepository>();

        }
    }
}
