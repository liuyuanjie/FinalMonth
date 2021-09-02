using System;
using System.Threading.Tasks;
using FinalMonth.gRPC.Client;
using Grpc.Net.Client;

namespace FinalMonth.gPRC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var text = Console.ReadLine();
            while ( text != null)
            {
                var reply = await client.SayHelloAsync(
                    new HelloRequest { Name = text });
                Console.WriteLine("Greeting: " + reply.Message);
                text = Console.ReadLine();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
