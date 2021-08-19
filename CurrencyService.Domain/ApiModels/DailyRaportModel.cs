using System;

namespace CurrencyService.Domain.ApiModels
{
    public class DailyRaportModel
    {
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
