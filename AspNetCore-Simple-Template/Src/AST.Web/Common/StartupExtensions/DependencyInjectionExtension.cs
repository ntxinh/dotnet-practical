using Microsoft.Extensions.DependencyInjection;
using AST.Core.Interfaces;
using AST.Infrastructure.Data;
using AST.Application.Interfaces;
using AST.Application.Services;

namespace AST.Web.Common.StartupExtensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCustomizedDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IFooRepository, FooRepository>();
            services.AddScoped<IFooService, FooService>();

            return services;
        }
    }
}
