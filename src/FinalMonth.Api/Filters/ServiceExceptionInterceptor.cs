using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.Filters
{
    public class ServiceExceptionInterceptor : IAsyncExceptionFilter
    {
        private readonly ILogger<ServiceExceptionInterceptor> _logger;

        public ServiceExceptionInterceptor(ILogger<ServiceExceptionInterceptor> logger)
        {
            _logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Service error:{0},{1},{2}", context.ModelState,context.HttpContext.Request.Path, context.HttpContext.User.FindFirstValue(ClaimTypes.Sid));
            //Business exception-More generics for external world
            var error = new
            {
                StatusCode = 500,
                Message = "Something went wrong! Internal Server Error by filter."
            };
            //Logs your technical exception with stack trace below

            context.Result = new JsonResult(error);
            return Task.CompletedTask;
        }
    }
}
