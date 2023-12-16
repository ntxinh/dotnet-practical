using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ASWA.Web.Extensions.Startup;

namespace ASWA.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCustomizedDatabase(Configuration);

            services.AddCustomizedAuth(Configuration);

            services.AddCustomizedAppInsights(Configuration);

            services.AddCustomizedStorage(Configuration);

            services.AddCustomizedHttp(Configuration);

            services.AddCustomizedDependencyInjection(Configuration);

            services.AddCustomizedSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomizedErrorHandling(env);

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseCustomizedAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomizedSwagger();
        }
    }
}
