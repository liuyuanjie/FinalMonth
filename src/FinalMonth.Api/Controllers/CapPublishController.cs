using System;
using DotNetCore.CAP;
using FinalMonth.Infrastructure.Data;
using FinalMonth.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapPublishController:Controller
    {
        private readonly IFinalMonthDBContext _dbContext;
        private readonly ICapPublisher _capPublisher;
        private readonly IGenericRepository<Infrastructure.Data.NotificationMessage> _repository;

        public CapPublishController(IFinalMonthDBContext dbContext, ICapPublisher capPublisher,IGenericRepository<Infrastructure.Data.NotificationMessage> repository)
        {
            _dbContext = dbContext;
            _capPublisher = capPublisher;
            _repository = repository;
        }

        [HttpPost]
        [Route("publish")]
        public IActionResult EntityFrameworkWithTransaction()
        {
            using (var trans = _dbContext.DbConnection.BeginTransaction(_capPublisher, autoCommit: true))
            {
                //your business logic code
                _repository.Create(new Infrastructure.Data.NotificationMessage
                {
                    From = "cap",
                    Message = DateTime.Now.ToString("O"),
                });
                _repository.UnitOfWOrk.CommitAsync();

                _capPublisher.Publish("xxx.services.show.time", DateTime.Now);
            }

            return Ok();
        }
    }
}
