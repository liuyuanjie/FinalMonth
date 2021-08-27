using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        public async Task<IActionResult> Test()
        {
            throw new ArgumentException("Test exception filter.");
        }

        [HttpGet]
        [Route("attribute")]
        [ServiceException]
        public async Task<IActionResult> TestAttribute()
        {
            throw new ArgumentException("Test exception attribute.");
        }

    }
}
