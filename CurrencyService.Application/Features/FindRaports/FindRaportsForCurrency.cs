using CurrencyService.Application.Features.FindRaport;
using CurrencyService.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.FindRaports
{
    public class FindRaportsForCurrency : IFindRaportsForCurrency
    {
        private readonly IFindRaportForCurrency _findRaportForCurrency;

        public FindRaportsForCurrency(IFindRaportForCurrency findRaportForCurrency)
        {
            _findRaportForCurrency = findRaportForCurrency;
        }

        public async Task<List<CurrencyRaportApiModel>> FindRaports(Dictionary<string, string> currencyCodes, DateTime start, DateTime end)
        {
            var output = new List<CurrencyRaportApiModel>();

            foreach(var code in currencyCodes)
            {
                output.Add(new CurrencyRaportApiModel()
                {
                    SourceCurrency = code.Key,
                    TargetCurrency = code.Value,
                    DailyRaports = await _findRaportForCurrency.FindRaport(code.Key, code.Value, start, end),
                });
            }

            return output;
        }
    }
}
