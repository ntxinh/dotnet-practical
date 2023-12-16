using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ASWA.Infra.Services.Storage;

namespace ASWA.Web.Extensions.Startup
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddCustomizedStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BlobStorageOptions>(configuration.GetSection("BlobStorageOptions"));
            services.AddSingleton<IBlobStorageService, BlobStorageService>();

            return services;
        }

        public static IApplicationBuilder UseCustomizedStorage(this IApplicationBuilder app)
        {
            return app;
        }
    }
}