using System.Threading.Tasks;
using FinalMonth.Api.NotificationMessage;
using Microsoft.AspNetCore.Mvc;

namespace FinalMonth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController
    {
        private readonly INotificationMessageHandler _notificationMessageHandler;

        public NotificationController(INotificationMessageHandler notificationMessageHandler)
        {
            _notificationMessageHandler = notificationMessageHandler;
        }

        [HttpPost]
        [Route("")]
        public async Task Send(string user, [FromBody] string message)
        {
            await _notificationMessageHandler.SendMessage(user, message);
        }

    }
}
