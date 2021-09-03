using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Domain;
using FinalMonth.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinalMonth.Application.Command
{
    public class SendNotificationMessageCommand : IRequest<bool>
    {
        public string User { get; set; }
        public string Message { get; set; }
    }

    public class SendNotificationMessageCommandHandler : IRequestHandler<SendNotificationMessageCommand, bool>
    {
        private readonly ILogger<SendNotificationMessageCommandHandler> _logger;
        private readonly IGenericRepository<NotificationMessage> _repository;

        public SendNotificationMessageCommandHandler(ILogger<SendNotificationMessageCommandHandler> logger, IGenericRepository<NotificationMessage> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<bool> Handle(SendNotificationMessageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{user} send {message} from client.", request.User, request.Message);
            _repository.Create(NotificationMessage.Create(request.User, request.Message));
            var result = await _repository.UnitOfWOrk.CommitAsync(cancellationToken);
            return result > 0;
        }
    }
}
