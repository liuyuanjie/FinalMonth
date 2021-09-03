﻿using System.Threading.Tasks;
using FinalMonth.Application.Command;
using MediatR;
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
            _logger.LogInformation("{type}:{user}:{message}", "send", user, message);
            return Clients.All.ReceiveMessage(user, message);
        }

        public Task ReceiveMessage(string user, string message)
        {
            _logger.LogInformation("{type}:{user}:{message}", "receive", user, message);

            return _mediator.Send(new SendNotificationMessageCommand()
            {
                User = user,
                Message = message
            });
        }
    }
}
