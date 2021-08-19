using CurrencyService.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace CurrencyService.Application.Services
{
    public class GetExternalApiBaseAddress : IGetExternalApiBaseAddress
    {
        HttpClient httpClient;

        public GetExternalApiBaseAddress(IConfiguration configuration)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration["ExternalApiLink"]);
        }

        public HttpClient GetHttpClient() => httpClient;
    }
}
