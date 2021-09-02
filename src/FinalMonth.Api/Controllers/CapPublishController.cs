using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using FinalMonth.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapPublishController:Controller
    {
        private readonly IFinalMonthDBContext _dbContext;
        private readonly ICapPublisher _capPublisher;

        public CapPublishController(IFinalMonthDBContext dbContext, ICapPublisher capPublisher)
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
        }

        [HttpPost]
        [Route("publish")]
        public IActionResult EntityFrameworkWithTransaction()
        {
            using (var trans = _dbContext.Database.BeginTransaction(_capPublisher, autoCommit: true))
            {
                //your business logic code

                _capPublisher.Publish("xxx.services.show.time", DateTime.Now);
            }

            return Ok();
        }
    }
}
