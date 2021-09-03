using System.Collections.Generic;
using System.Threading.Tasks;
using FinalMonth.Api.NotificationMessage;
using FinalMonth.Application.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController
    {
        private readonly INotificationMessageHandler _notificationMessageHandler;
        private readonly INotificationMessageQuery _notificationMessageQuery;

        public NotificationController(INotificationMessageHandler notificationMessageHandler, INotificationMessageQuery notificationMessageQuery)
        {
            _notificationMessageHandler = notificationMessageHandler;
            _notificationMessageQuery = notificationMessageQuery;
        }

        [HttpPost]
        [Route("")]
        public async Task Send(string user, [FromBody] string message) {
            await _notificationMessageHandler.SendMessage(user, message);
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<Domain.NotificationMessage>> Get()
        {
            return await _notificationMessageQuery.GetAllAsync();
        }

        [HttpGet]
        [Route("ten")]
        public async Task<IList<Domain.NotificationMessage>> GetTen()
        {
            return await _notificationMessageQuery.GetTopTenAsync();
        }

        [HttpGet]
        [Route("one")]
        public async Task<IList<Domain.NotificationMessage>> GetOne()
        {
            return await _notificationMessageQuery.GetTopOneAsync();
        }
    }
}