using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Application.Notification
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
            _logger.LogInformation("publish new member message notification.");
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
            _logger.LogInformation("publish new member email notification.");
            return Task.CompletedTask;
        }
    }
}
