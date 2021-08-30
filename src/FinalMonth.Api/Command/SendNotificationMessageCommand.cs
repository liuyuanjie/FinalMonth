using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Api.Command
{
    public class SendNotificationMessageCommand : IRequest<bool>
    {
        public string User { get; set; }
        public string Message { get; set; }
    }

    public class SendNotificationMessageCommandHandler : IRequestHandler<SendNotificationMessageCommand, bool>
    {
        private readonly ILogger<SendNotificationMessageCommandHandler> _logger;

        public SendNotificationMessageCommandHandler(ILogger<SendNotificationMessageCommandHandler> logger)
        {
            _logger = logger;
        }
        public async Task<bool> Handle(SendNotificationMessageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{user} send {message} from client.", request.User, request.Message);

            return true;
        }
    }
}
