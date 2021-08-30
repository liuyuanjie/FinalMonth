using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.Notification
{
    public class SendMemberNotification : INotification
    {
        public Member Member { get; set; }
    }

    public class SendMemberNotificationHandler : INotificationHandler<SendMemberNotification>
    {
        private readonly ILogger<SendMemberNotification> _logger;

        public SendMemberNotificationHandler(ILogger<SendMemberNotification> logger)
        {
            _logger = logger;
        }

        public Task Handle(SendMemberNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("handle notification from notification");
            return Task.CompletedTask;
        }
    }

    public class SendMemberSendingLogNotificationHandler : INotificationHandler<SendMemberNotification>
    {
        private readonly ILogger<SendMemberNotification> _logger;

        public SendMemberSendingLogNotificationHandler(ILogger<SendMemberNotification> logger)
        {
            _logger = logger;
        }

        public Task Handle(SendMemberNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("handle notification from sending email.");
            return Task.CompletedTask;
        }
    }
}
