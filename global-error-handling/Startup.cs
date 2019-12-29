using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace global_error_handling
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                // Approach 1:
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                // Approach 1:
                // app.UseExceptionHandler("/error");
                // Approach 2:
                // app.UseExceptionHandler(a => a.Run(async context =>
                // {
                //     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //     context.Response.ContentType = "application/json";

                //     var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                //     var exception = feature.Error;

                //     var result = JsonSerializer.Serialize(new { error = exception.Message });
                //     // var result = new ErrorDetails()
                //     // {
                //     //     StatusCode = context.Response.StatusCode,
                //     //     Message = "Internal Server Error."
                //     // }.ToString();
                //     await context.Response.WriteAsync(result);
                // }));
                // Approach 3:
                app.UseMiddleware<ExceptionMiddleware>();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
