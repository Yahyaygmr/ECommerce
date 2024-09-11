using ECommerce.Api.Application.Services;
using ECommerce.Api.Infrastructure.Services;
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
            services.AddScoped<IFileService, FileService>();
        }
    }
}
