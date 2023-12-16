using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ASWA.Infra.Data;

namespace ASWA.Web.Extensions.Startup
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(
                    options, configuration.GetValue<string>("MSSQLDB_CONNECTION")));
            
            services.AddDbContext<CosmosDbContext>(options => options.UseCosmos(
                accountEndpoint: configuration.GetValue<string>("COSMOSDB:URI"),
                accountKey: configuration.GetValue<string>("COSMOSDB:KEY"),
                databaseName: configuration.GetValue<string>("COSMOSDB:DATABASENAME"))
            );

            return services;
        }

        public static IApplicationBuilder UseCustomizedDatabase(this IApplicationBuilder app)
        {
            return app;
        }
    }
}