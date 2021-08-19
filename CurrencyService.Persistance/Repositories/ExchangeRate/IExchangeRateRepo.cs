using CurrencyService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyService.Persistance.Repositories.ExchangeRate
{
    public interface IExchangeRateRepo
    {
        Task<List<ExchangeRateModel>> Get(DateTime start, DateTime end, string from, string to);
        Task<List<ExchangeRateModel>> GetAll();
        Task UpdateMany(List<ExchangeRateModel> exchangeRateModels);
        Task AddMany(List<ExchangeRateModel> exchangeRateModels);
    }
}