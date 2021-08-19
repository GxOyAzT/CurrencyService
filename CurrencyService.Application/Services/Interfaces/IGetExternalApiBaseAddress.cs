using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace CurrencyService.Application.Services.Interfaces
{
    public interface IGetExternalApiBaseAddress
    {
        HttpClient GetHttpClient();
    }
}