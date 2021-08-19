using CurrencyService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyService.Application.Features.ConvertDailyReport
{
    public class ConvertCsvRaportToList : IConvertCsvRaportToList
    {
        public List<ExchangeRateModel> ConvertCsvToList(string fromCSV)
        {
            var lines = fromCSV.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var output = new List<ExchangeRateModel>();

            foreach (var item in lines)
            {
                string[] values = item.Split(',');

                if (values[7] == "\r")
                    continue;

                var model = new ExchangeRateModel
                {
                    SourceCurrency = values[2],
                    TargetCurrency = values[3],
                    FromDate = Convert.ToDateTime(values[6]),
                    Currency = Convert.ToDecimal(values[7])
                };

                output.Add(model);
            }

            return output;
        }
    }
}