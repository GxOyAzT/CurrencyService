using CurrencyService.API.Utilities.Token;
using CurrencyService.Application.BackgroundTasks;
using CurrencyService.Application.Features.ConvertDailyReport;
using CurrencyService.Application.Features.FindRaport;
using CurrencyService.Application.Features.FindRaports;
using CurrencyService.Application.Features.GetExternalApiRaport;
using CurrencyService.Application.Services;
using CurrencyService.Application.Services.Interfaces;
using CurrencyService.Persistance.Repositories.ExchangeRate;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyService.API.Utilities
{
    public static class ConfigureServices
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRateRepo, ExchangeRateRepo>();
            services.AddScoped<IGetRaportFromExternalAPI, GetRaportFromExternalAPI>();
            services.AddScoped<IConvertCsvRaportToList, ConvertCsvRaportToList>();
            services.AddScoped<IGetExternalApiBaseAddress, GetExternalApiBaseAddress>();
            services.AddScoped<ISyncExternalApi, SyncExternalApi>();
            services.AddScoped<IGetApiToken, GetApiToken>();
            services.AddScoped<IFindRaportForCurrency, FindRaportForCurrency>();
            services.AddScoped<IFindRaportsForCurrency, FindRaportsForCurrency>();
        }
    }
}
