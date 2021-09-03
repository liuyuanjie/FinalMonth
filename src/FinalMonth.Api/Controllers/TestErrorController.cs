using System;
using System.Threading.Tasks;
using FinalMonth.Api.CustomAttributes;
using FinalMonth.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestErrorController : Controller
    {
        [TypeFilter(typeof(ServiceExceptionInterceptor))]
        [HttpGet]
        public Task<IActionResult> Test()
        {
            throw new ArgumentException("Test exception filter.");
        }

        [HttpGet]
        [Route("attribute")]
        [ServiceException]
        public Task<IActionResult> TestAttribute()
        {
            throw new ArgumentException("Test exception attribute.");
        }

        [HttpGet]
        [Route("middleware")]
        //[ServiceException]
        public Task<IActionResult> TestMiddleware()
        {
            throw new ArgumentException("Test exception attribute.");
        }
    }
}
