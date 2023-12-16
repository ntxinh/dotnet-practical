using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ASWA.Core.Interfaces;
using ASWA.Infra.Data;
using ASWA.Web.Interfaces;
using ASWA.Web.Services;

namespace ASWA.Web.Extensions.Startup
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCustomizedDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerViewModelService, CustomerViewModelService>();

            return services;
        }

        public static IApplicationBuilder UseCustomizedDependencyInjection(this IApplicationBuilder app)
        {
            return app;
        }
    }
}