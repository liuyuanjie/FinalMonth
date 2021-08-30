using System.Threading.Tasks;

namespace FinalMonth.Api.SignalR
{
    public interface INotificationMessage
    {
        Task SendMessage(string user, string message);
        void ReceiveMessage(string user, string message);
    }
}