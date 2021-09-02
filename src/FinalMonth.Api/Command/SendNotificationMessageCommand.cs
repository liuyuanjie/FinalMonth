using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data;
using FinalMonth.Infrastructure.Repository;
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
        private readonly IGenericRepository<Infrastructure.Data.NotificationMessage> _repository;

        public SendNotificationMessageCommandHandler(ILogger<SendNotificationMessageCommandHandler> logger, IGenericRepository<Infrastructure.Data.NotificationMessage> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<bool> Handle(SendNotificationMessageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{user} send {message} from client.", request.User, request.Message);
            _repository.Create(new Infrastructure.Data.NotificationMessage
            {
                From = request.User,
                Message = request.Message
            });
            var result = await _repository.UnitOfWOrk.CommitAsync(cancellationToken);
            return result > 0;
        }
    }
}
