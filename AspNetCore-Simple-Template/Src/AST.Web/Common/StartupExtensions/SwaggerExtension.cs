using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AST.Web.Common.StartupExtensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Register the Swagger services
                services.AddSwaggerDocument();
            }

            return services;
        }

        public static IApplicationBuilder UseCustomizedSwagger(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Register the Swagger generator and the Swagger UI middlewares
                app.UseOpenApi();
                app.UseSwaggerUi3(x => x.DocExpansion = "list");
            }

            return app;
        }
    }
}
