using System;
using System.Collections.Generic;

namespace CurrencyService.Domain.ApiModels
{
    public class CurrencyRequestApiModel
    {
        public Dictionary<string, string> CurrencyCodes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
