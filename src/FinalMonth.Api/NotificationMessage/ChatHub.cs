using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.NotificationMessage
{
    public class NotificationMessageHub : Hub
    {
        private readonly ILogger<NotificationMessageHub> _logger;

        public NotificationMessageHub(ILogger<NotificationMessageHub> logger)
        {
            _logger = logger;
        }

        public Task SendMessage(string user, string message)
        {

            return Clients.All.SendAsync("SendMessage", user, message);
        }

        public void ReceiveMessage(string user, string message)
        {
            _logger.LogInformation("{user}:{message}", user, message);
        }
    }
}
