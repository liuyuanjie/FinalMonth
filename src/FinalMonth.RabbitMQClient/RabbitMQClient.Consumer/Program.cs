using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQClient.Consumer
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
                VirtualHost = _configuration.GetSection("RabbitMQ:VirtualHost").Value,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("FinalMonth", false, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("FinalMonth", true, consumer);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                Console.WriteLine("Has received： {0}", message);
            };
            Console.ReadLine();
        }
    }
}
