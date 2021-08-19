using CurrencyService.Application.Features.GetExternalApiRaport;
using CurrencyService.Persistance.Repositories.ExchangeRate;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyService.Application.BackgroundTasks
{
    public class SyncExternalApi : ISyncExternalApi
    {
        private readonly IExchangeRateRepo _exchangeRateRepo;
        private readonly IGetRaportFromExternalAPI _getRaportFromExternalAPI;

        public SyncExternalApi(
            IExchangeRateRepo exchangeRateRepo,
            IGetRaportFromExternalAPI getRaportFromExternalAPI)
        {
            _exchangeRateRepo = exchangeRateRepo;
            _getRaportFromExternalAPI = getRaportFromExternalAPI;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var exchanges = await _exchangeRateRepo.GetAll();
                DateTime getFrom = exchanges.Any() ? exchanges.OrderBy(e => e.FromDate).LastOrDefault().FromDate.AddDays(1) : DateTime.MinValue;

                var newExchangeRates = await _getRaportFromExternalAPI.GetRaport(getFrom, DateTime.Now);

                if (newExchangeRates == null)
                    continue;

                await _exchangeRateRepo.AddMany(newExchangeRates);

                await Task.Delay(TimeSpan.FromHours(1));
            }
        }
    }
}
