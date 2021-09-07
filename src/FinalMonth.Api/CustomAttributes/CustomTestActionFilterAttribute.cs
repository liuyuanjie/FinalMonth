using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.Filters
{
    public class CustomTestActionAttribute : ActionFilterAttribute
    {
        private ILogger<CustomTestActionAttribute> _logger;

        public CustomTestActionAttribute()
        {
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            _logger = (ILogger<CustomTestActionAttribute>)context.HttpContext.RequestServices.GetService(typeof(ILogger<CustomTestActionAttribute>));
            _logger.LogWarning("Controller before");
            await next();
            _logger.LogWarning("Controller after");
        }
    }
}
