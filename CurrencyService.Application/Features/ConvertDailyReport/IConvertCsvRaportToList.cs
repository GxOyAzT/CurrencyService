using CurrencyService.Domain.Models;
using System;
using System.Collections.Generic;

namespace CurrencyService.Application.Features.ConvertDailyReport
{
    public interface IConvertCsvRaportToList
    {
        List<ExchangeRateModel> ConvertCsvToList(string fromCSV);
    }
}