using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalMonth.Api.NotificationMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
