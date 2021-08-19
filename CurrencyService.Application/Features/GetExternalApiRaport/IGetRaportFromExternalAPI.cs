using CurrencyService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.GetExternalApiRaport
{
    public interface IGetRaportFromExternalAPI
    {
        Task<List<ExchangeRateModel>> GetRaport(DateTime dateFrom, DateTime dateTo);
    }
}