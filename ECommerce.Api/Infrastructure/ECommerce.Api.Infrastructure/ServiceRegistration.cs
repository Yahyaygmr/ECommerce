using ECommerce.Api.Application.Abstraction.Storage;
using ECommerce.Api.Application.Abstraction.Token;
using ECommerce.Api.Infrastructure.Services;
using ECommerce.Api.Infrastructure.Services.Storage;
using ECommerce.Api.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void InfrastructureServiceRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();

        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
