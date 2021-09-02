using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQClient.Producer
{
    class Program
    {
        private static IConfiguration _configuration;
        static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetSection("RabbitMQ:HostName").Value,
                UserName = _configuration.GetSection("RabbitMQ:UserName").Value,
                Password = _configuration.GetSection("RabbitMQ:Password").Value,
                Port = int.Parse(_configuration.GetSection("RabbitMQ:EndPoint").Value),
                VirtualHost = _configuration.GetSection("RabbitMQ:VirtualHost").Value
            };
            //factory.VirtualHost = _configuration.GetSection("RabbitMQ:VirtualHost").Value;
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            while (true)
            {
                var body = Encoding.UTF8.GetBytes(Console.ReadLine());
                channel.BasicPublish("", "FinalMonth", null, body);
            }
        }
    }
}
