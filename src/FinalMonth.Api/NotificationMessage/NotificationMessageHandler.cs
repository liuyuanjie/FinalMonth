using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.NotificationMessage
{
    public class NotificationMessageHandler : INotificationMessageHandler
    {
        private readonly IHubContext<NotificationMessageHub, INotificationMessageHub> _hubContext;
        private readonly ILogger<NotificationMessageHandler> _logger;

        public NotificationMessageHandler(IHubContext<NotificationMessageHub, INotificationMessageHub> hubContext, ILogger<NotificationMessageHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public Task SendMessage(string user, string message)
        {
            _logger.LogInformation("{user} send {message} from server.", user, message);
            return _hubContext.Clients.All.ReceiveMessage(user, message);
        }
    }
}
