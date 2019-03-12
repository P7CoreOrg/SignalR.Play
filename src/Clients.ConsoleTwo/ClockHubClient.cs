using System;
using System.Threading;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace Clients.ConsoleTwo
{
    public partial class ClockHubClient : IClock, IHostedService
    {
        #region ClockHubClientCtor
        private readonly ILogger<ClockHubClient> _logger;
        private HubConnection _connection;

        public ClockHubClient(ILogger<ClockHubClient> logger)
        {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl(Wellknown.ClockHubUrl)
                .Build();

            _connection.On<DateTime>(Wellknown.Events.TimeSent,
                dateTime => _ = ShowTime(dateTime));
        }

        public Task ShowTime(DateTime currentTime)
        {
            _logger.LogInformation($"{currentTime.ToShortTimeString()}");

            return Task.CompletedTask;
        }
        #endregion

        #region StartAsync
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Loop is here to wait until the server is running
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);

                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
        }
        #endregion
        #region StopAsync
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _connection.DisposeAsync();
        }
        #endregion
    }
}