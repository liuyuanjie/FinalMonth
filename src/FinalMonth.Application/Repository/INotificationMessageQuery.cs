using System.Collections.Generic;
using System.Threading.Tasks;
using FinalMonth.Domain;

namespace FinalMonth.Application.Repository
{
    public interface INotificationMessageQuery : IGenericQuery<NotificationMessage>
    {
        Task<List<NotificationMessage>> GetTopTenAsync();
        Task<List<NotificationMessage>> GetTopOneAsync();
    }
}
