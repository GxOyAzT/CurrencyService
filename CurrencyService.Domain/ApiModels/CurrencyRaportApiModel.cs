using System.Collections.Generic;

namespace CurrencyService.Domain.ApiModels
{
    public class CurrencyRaportApiModel
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public List<DailyRaportModel> DailyRaports { get; set; }
    }
}
