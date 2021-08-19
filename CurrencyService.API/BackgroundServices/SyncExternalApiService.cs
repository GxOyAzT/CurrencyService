using CurrencyService.Application.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyService.API.BackgroundServices
{
    public class SyncExternalApiService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public SyncExternalApiService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _services.CreateScope())
            {
                var getDataService =
                    scope.ServiceProvider
                        .GetRequiredService<ISyncExternalApi>();

                await getDataService.Start(stoppingToken);
            }
        }
    }
}
