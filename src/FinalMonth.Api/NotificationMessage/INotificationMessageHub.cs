using System.Threading.Tasks;

namespace FinalMonth.Api.NotificationMessage
{
    public interface INotificationMessageHub
    {
        Task ReceiveMessage(string user, string message);
    }
}