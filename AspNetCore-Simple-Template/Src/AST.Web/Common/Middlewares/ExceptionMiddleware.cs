using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AST.Core.Common.ResponseBuilder;
using Microsoft.AspNetCore.Http;

namespace AST.Web.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int code;
            string msg;

            // if (ex is RequestRateTooLargeException) code = ...;
            var exceptionType = ex.GetType();
            switch (ex)
            {
                // case Exception e when exceptionType == typeof(RequestRateTooLargeException):
                //     code = AppConstant.RequestRateTooLargeCode;
                //     msg = AppConstant.RequestRateTooLarge;
                //     break;
                default:
                    code = (int)HttpStatusCode.InternalServerError;
                    msg = "Internal Server Error";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            var res = new ApiResponseBuilder<object>().Error(msg).DebugMessage(ex.Message).Build();
            var result = JsonSerializer.Serialize(res, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(result);
        }
    }
}
