using System;
using System.ComponentModel.DataAnnotations;
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
        public Task<IActionResult> Test(int age)
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

        [TypeFilter(typeof(ServiceExceptionInterceptor))]
        [HttpPost]
        [Route("validate")]
        public Task<IActionResult> TestValidate(TestValidate validate)
        {
            throw new ArgumentException("Test exception filter.");
        }
    }

    public class TestValidate
    {
        [Required]
        [Range(50,80)]
        public int Age { get; set; }
    }
}
