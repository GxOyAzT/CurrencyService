using System;

namespace CurrencyService.Domain.Models
{
    public class ExchangeRateModel
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public DateTime FromDate { get; set; }
        public decimal Currency { get; set; }
    }
}
