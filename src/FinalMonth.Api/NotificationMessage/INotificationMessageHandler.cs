using System.Threading.Tasks;

namespace FinalMonth.Api.NotificationMessage
{
    public interface INotificationMessageHandler
    {
        Task SendMessage(string user, string message);
    }
}