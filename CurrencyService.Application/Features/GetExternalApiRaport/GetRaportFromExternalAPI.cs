using CurrencyService.Application.Features.ConvertDailyReport;
using CurrencyService.Application.Services.Interfaces;
using CurrencyService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CurrencyService.Application.Features.GetExternalApiRaport
{
    public class GetRaportFromExternalAPI : IGetRaportFromExternalAPI
    {
        private readonly IGetExternalApiBaseAddress _externalApiBaseAddress;
        private readonly IConvertCsvRaportToList _convertDailyReportToList;

        public GetRaportFromExternalAPI(
            IGetExternalApiBaseAddress externalApiBaseAddress,
            IConvertCsvRaportToList convertDailyReportToList)
        {
            _externalApiBaseAddress = externalApiBaseAddress;
            _convertDailyReportToList = convertDailyReportToList;
        }

        public async Task<List<ExchangeRateModel>> GetRaport(DateTime dateFrom, DateTime dateTo)
        {
            var client = _externalApiBaseAddress.GetHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));

            var response = await client.GetAsync($"/service/data/EXR/D...SP00.A?startPeriod={dateFrom.ToString("yyyy-MM-dd")}&endPeriod={dateTo.ToString("yyyy-MM-dd")}&detail=dataonly");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var responseStringContext = await response.Content.ReadAsStringAsync();

            var exchangeRateModels = _convertDailyReportToList.ConvertCsvToList(responseStringContext);

            return exchangeRateModels;
        }
    }
}
