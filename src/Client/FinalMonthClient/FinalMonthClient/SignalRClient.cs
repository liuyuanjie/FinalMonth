using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace FinalMonthClient
{
    public class SignalRClient
    {
        private readonly HubConnection _connection;

        public SignalRClient()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/notificationmessagehub")
                .Build();

            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }

        public async Task Connect()
        {
            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"{user}:{message}");
            });

            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task SendMessage(string user, string message)
        {
            try
            {
                await _connection.InvokeAsync("ReceiveMessage",
                    user, message);
            }
            catch (Exception ex)
            {
            }
        }

        public void ReceiveMessage(string user, string message)
        {
            try
            {
                Console.WriteLine($"{user}:{message}"); ;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
