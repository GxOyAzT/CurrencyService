using CurrencyService.Domain.ApiModels;
using CurrencyService.Persistance.Repositories.ExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.FindRaport
{
    public class FindRaportForCurrency : IFindRaportForCurrency
    {
        private readonly IExchangeRateRepo _exchangeRateRepo;

        public FindRaportForCurrency(IExchangeRateRepo exchangeRateRepo)
        {
            _exchangeRateRepo = exchangeRateRepo;
        }

        public async Task<List<DailyRaportModel>> FindRaport(string sourceCurrency, string targetCurrency, DateTime start, DateTime end)
        {
            var output = new List<DailyRaportModel>();

            var rates = await _exchangeRateRepo.Get(start, end, sourceCurrency, targetCurrency);

            if (rates.Any())
            {
                rates.ForEach(e => output.Add(new DailyRaportModel()
                {
                    Date = e.FromDate,
                    Value = e.Currency
                }));
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
