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
        private readonly IFinalMonthDataContext _dataContext;
        private readonly ICapPublisher _capPublisher;

        public CapPublishController(IFinalMonthDataContext dataContext, ICapPublisher capPublisher)
        {
            _dataContext = dataContext;
            _capPublisher = capPublisher;
        }

        [HttpPost]
        [Route("publish")]
        public IActionResult EntityFrameworkWithTransaction()
        {
            using (var trans = _dataContext.Database.BeginTransaction(_capPublisher, autoCommit: true))
            {
                //your business logic code

                _capPublisher.Publish("xxx.services.show.time", DateTime.Now);
            }

            return Ok();
        }
    }
}
