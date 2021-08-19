using CurrencyService.Domain.Models;
using CurrencyService.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.Persistance.Repositories.ExchangeRate
{
    public class ExchangeRateRepo : IExchangeRateRepo
    {
        private readonly AppDbContext _appDbContext;

        public ExchangeRateRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddMany(List<ExchangeRateModel> exchangeRateModels)
        {
            _appDbContext.ExchangeRates.AddRange(exchangeRateModels);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateMany(List<ExchangeRateModel> exchangeRateModels)
        {
            _appDbContext.ExchangeRates.UpdateRange(exchangeRateModels);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<ExchangeRateModel>> Get(DateTime start, DateTime end, string from, string to)
        {
            return _appDbContext.ExchangeRates
                .Where(e => e.SourceCurrency == from && e.TargetCurrency == to && e.FromDate >= start && e.FromDate <= end)
                .ToList();
        }

        public async Task<List<ExchangeRateModel>> GetAll() => await _appDbContext.ExchangeRates.ToListAsync();
    }
}
