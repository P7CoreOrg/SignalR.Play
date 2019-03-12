using System;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Clients.ConsoleOne
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(Wellknown.ClockHubUrl)
                .Build();

            connection.On<DateTime>(Wellknown.Events.TimeSent, (dateTime) =>
            {
                Console.WriteLine(dateTime.ToString());
            });
            // Loop is here to wait until the server is running
            while (true)
            {
                try
                {
                    await connection.StartAsync();

                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }

            Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            Console.ReadLine();
        }
    }
}
