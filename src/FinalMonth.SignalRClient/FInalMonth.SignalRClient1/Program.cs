using System;

namespace FinalMonthClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var signalRClient = new SignalRClient(args[0]);
            signalRClient.Connect().Wait();
            signalRClient.SendMessage("kaka", "test").Wait();
            
            Console.WriteLine("Hello World!");

            while (true)
            {
                var content = Console.ReadLine();
                signalRClient.SendMessage("kaka", content).Wait();
            }
        }
    }
}
