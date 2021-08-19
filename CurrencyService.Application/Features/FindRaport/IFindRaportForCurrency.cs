using CurrencyService.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.FindRaport
{
    public interface IFindRaportForCurrency
    {
        public Task<List<DailyRaportModel>> FindRaport(string sourceCurrency, string targetCurrency, DateTime start, DateTime end);
    }
}
