using System.Threading.Tasks;
using FinalMonth.Api.Command;
using MediatR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.NotificationMessage
{ 
    //[HubName("NotificationMessageHub")]
    public class NotificationMessageHub : Hub<INotificationMessageHub>
    {
        private readonly ILogger<NotificationMessageHub> _logger;
        private readonly IMediator _mediator;

        public NotificationMessageHub(ILogger<NotificationMessageHub> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Task SendMessage(string user, string message)
        {

            return Clients.All.ReceiveMessage(user, message);
        }

        public Task ReceiveMessage(string user, string message)
        {
            _mediator.Send(new SendNotificationMessageCommand()
            {
                User = user,
                Message = message
            });
            return Task.CompletedTask;
        }
    }
}
