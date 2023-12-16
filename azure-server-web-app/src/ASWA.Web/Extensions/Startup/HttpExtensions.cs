using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASWA.Web.Extensions.Startup
{
    public static class HttpExtensions
    {
        public static IServiceCollection AddCustomizedHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("HttpServerA", c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("HttpServerA").Value);
            });

            return services;
        }

        public static IApplicationBuilder UseCustomizedHttp(this IApplicationBuilder app)
        {
            return app;
        }
    }
}