using CurrencyService.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.FindRaports
{
    public interface IFindRaportsForCurrency
    {
        public Task<List<CurrencyRaportApiModel>> FindRaports(Dictionary<string, string> currencyCodes, DateTime start, DateTime end);
    }
}
