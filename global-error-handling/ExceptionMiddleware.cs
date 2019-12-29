using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace global_error_handling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // if      (ex is MyNotFoundException)     code = HttpStatusCode.NotFound;
            // else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (ex is MyException)             code = HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var result = JsonSerializer.Serialize(new { error = ex.Message });

            return context.Response.WriteAsync(result);
        }
    }
}
