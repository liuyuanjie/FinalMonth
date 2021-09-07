using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.Filters
{
    public class CustomTestActionFilter : IAsyncActionFilter
    {
        private ILogger<CustomTestActionFilter> _logger;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger = (ILogger<CustomTestActionFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<CustomTestActionFilter>));

            _logger.LogWarning("Controller filter before");
            await next();
            _logger.LogWarning("Controller filter after");
        }
    }
}
