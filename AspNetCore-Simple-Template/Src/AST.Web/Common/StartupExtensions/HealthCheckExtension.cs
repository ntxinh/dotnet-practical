using AST.Infrastructure.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AST.Web.Common.StartupExtensions
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddCustomizedHealthCheck(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // if (env.IsProduction() || env.IsStaging())
            // {
                services
                    .AddHealthChecks()
                    .AddSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .AddDbContextCheck<AppDbContext>();
                services
                    .AddHealthChecksUI(setupSettings: setup =>
                    {
                        setup.AddHealthCheckEndpoint("endpoint1", "/healthz");
                    })
                    .AddInMemoryStorage();
            // }

            return services;
        }

        public static void UseCustomizedHealthCheck(IEndpointRouteBuilder endpoints, IWebHostEnvironment env)
        {
            // if (env.IsProduction() || env.IsStaging())
            // {
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();
            // }
        }
    }
}
