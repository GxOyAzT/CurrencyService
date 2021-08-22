using CurrencyService.Application.Extensions;
using CurrencyService.Domain.ApiModels;
using CurrencyService.Persistance.Repositories.ExchangeRate;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.FindRaport
{
    public class FindRaportForCurrency : IFindRaportForCurrency
    {
        private readonly IExchangeRateRepo _exchangeRateRepo;
        private readonly IDistributedCache _cache;

        public FindRaportForCurrency(
            IExchangeRateRepo exchangeRateRepo,
            IDistributedCache cache)
        {
            _exchangeRateRepo = exchangeRateRepo;
            _cache = cache;
        }

        public async Task<List<DailyRaportModel>> FindRaport(string sourceCurrency, string targetCurrency, DateTime start, DateTime end)
        {
            var output = new List<DailyRaportModel>();

            var fromCache = await _cache.GetRecordAsync<List<DailyRaportModel>>($"FindRaport_Source={sourceCurrency}_Tagret={targetCurrency}_start={start.ToString("yyyyMMdd")}_end={end.ToString("yyyyMMdd")}");

            if (fromCache is not null)
            {
                return fromCache;
            }

            var rates = await _exchangeRateRepo.Get(start, end, sourceCurrency, targetCurrency);

            if (rates.Any())
            {
                rates.ForEach(e => output.Add(new DailyRaportModel()
                {
                    Date = e.FromDate,
                    Value = e.Currency
                }));

                _cache.SetRecordAsync<List<DailyRaportModel>>($"FindRaport_Source={sourceCurrency}_Tagret={targetCurrency}_start={start.ToString("yyyyMMdd")}_end={end.ToString("yyyyMMdd")}",
                    output, TimeSpan.FromHours(1), TimeSpan.FromMinutes(5));
            }

            if (!rates.Any())
            {
                rates = await _exchangeRateRepo.Get(DateTime.MinValue, start, sourceCurrency, targetCurrency);
                rates = rates.OrderBy(e => e.FromDate).ToList();

                if (rates.Any())
                {
                    output.Add(new DailyRaportModel()
                    {
                        Date = rates.LastOrDefault().FromDate,
                        Value = rates.LastOrDefault().Currency
                    });
                }
            }

            return output;
        }
    }
}
