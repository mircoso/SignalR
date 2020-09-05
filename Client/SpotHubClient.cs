using System;
using System.Threading;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Client
{
    public partial class SpotHubClient : ISpot, IHostedService
    {
        private readonly ILogger<SpotHubClient> _logger;
        private HubConnection _connection;

        public SpotHubClient(ILogger<SpotHubClient> logger)
        {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubSpotUrl)
                .Build();

            _connection.Closed += ConnectionSpot_Closed;
            _connection.Reconnecting += ConnectionSpot_Reconnecting;
            _connection.Reconnected += ConnectionSpot_Reconnected;

            _connection.On(Strings.SpotEvents.SpotSent, OnlySpot);
        }

        public Task SetName(string name)
        {
            _logger.LogInformation("Spot");

            return Task.CompletedTask;
        }

        public Task OnlySpot()
        {
            _logger.LogInformation("OnlySpot");

            return Task.CompletedTask;
        }

        private static async Task ConnectionSpot_Reconnected(string arg)
        {
            await Task.Run(() => { Console.WriteLine($"Reconnected."); });
        }

        private static async Task ConnectionSpot_Reconnecting(Exception arg)
        {
            await Task.Run(() => { Console.WriteLine($"Reconnecting."); });
        }

        private static async Task ConnectionSpot_Closed(Exception arg)
        {
            await Task.Run(() => { Console.WriteLine($"Closed."); });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Loop is here to wait until the server is running
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);

                    await _connection.InvokeAsync(Strings.SpotEvents.SetNameSent, Environment.MachineName);

                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _connection.DisposeAsync();
        }
    }
}