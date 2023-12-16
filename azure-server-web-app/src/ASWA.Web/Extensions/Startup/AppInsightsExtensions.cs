using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;

namespace ASWA.Web.Extensions.Startup
{
    public static class AppInsightsExtensions
    {
        public static IServiceCollection AddCustomizedAppInsights(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TelemetryConfiguration>((o) =>
            {
                o.InstrumentationKey = configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY");
                o.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            });

            return services;
        }

        public static IApplicationBuilder UseCustomizedAppInsights(this IApplicationBuilder app)
        {
            return app;
        }
    }
}