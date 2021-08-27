using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.CustomMiddlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next) //ILogger<CustomExceptionMiddleware> logger
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger<CustomExceptionMiddleware>();
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Catch exception by {0}", "custom exception middle ware");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.Body.WriteAsync(Encoding.Default.GetBytes("Please contact the system admin."));
            }
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
