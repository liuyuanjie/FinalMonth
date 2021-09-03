using System;
using DotNetCore.CAP;
using FinalMonth.Application.Repository;
using FinalMonth.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CapPublishController : Controller
    {
        private readonly IFinalMonthDBContext _dbContext;
        private readonly ICapPublisher _capPublisher;
        private readonly IGenericRepository<Domain.NotificationMessage> _repository;

        public CapPublishController(IFinalMonthDBContext dbContext, ICapPublisher capPublisher, IGenericRepository<Domain.NotificationMessage> repository)
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
                var notificationMessage = new Domain.NotificationMessage
                {
                    From = "cap",
                    Message = DateTime.Now.ToString("O"),
                };
                _repository.Create(notificationMessage);
                _repository.UnitOfWOrk.CommitAsync();

                _capPublisher.Publish("xxx.services.show.time", notificationMessage.ToString());
            }

            return Ok();
        }
    }
}
