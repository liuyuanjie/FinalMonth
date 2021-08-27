using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalMonth.Api.CustomAttributes
{
    public class ServiceExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //Business exception-More generics for external world
            var error = new
            {
                StatusCode = 500,
                Message = "Something went wrong! Internal Server Error by attribute."
            };
            //Logs your technical exception with stack trace below

            context.Result = new JsonResult(error);
            base.OnException(context);
        }
    }
}
