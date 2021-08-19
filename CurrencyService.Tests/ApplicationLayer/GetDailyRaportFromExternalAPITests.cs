using CurrencyService.Application.Features.ConvertDailyReport;
using CurrencyService.Application.Features.GetExternalApiRaport;
using CurrencyService.Application.Services.Interfaces;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyService.Tests.ApplicationLayer
{
    public class GetDailyRaportFromExternalAPITests
    {
        [Fact]
        public async Task TestA()
        {
            var getExternalApiBaseAddress = new Mock<IGetExternalApiBaseAddress>();
            getExternalApiBaseAddress.Setup(s => s.GetHttpClient())
                .Returns(new System.Net.Http.HttpClient() { BaseAddress = new System.Uri("https://sdw-wsrest.ecb.europa.eu") });

            var testedUnit = GetUtilityForTest(getExternalApiBaseAddress.Object, new ConvertCsvRaportToList());

            var output = await testedUnit.GetRaport(new DateTime(2000,05,05), new DateTime(2000, 05, 05));

            Assert.Equal(1.5467m, output.FirstOrDefault(e => e.SourceCurrency == "CHF" && e.TargetCurrency == "EUR").Currency);
            Assert.Equal(36.603m, output.FirstOrDefault(e => e.SourceCurrency == "CZK" && e.TargetCurrency == "EUR").Currency);
        }

        [Fact]
        public async Task TestB()
        {
            var getExternalApiBaseAddress = new Mock<IGetExternalApiBaseAddress>();
            getExternalApiBaseAddress.Setup(s => s.GetHttpClient())
                .Returns(new System.Net.Http.HttpClient() { BaseAddress = new System.Uri("https://sdw-wsrest.ecb.europa.eu") });

            var testedUnit = GetUtilityForTest(getExternalApiBaseAddress.Object, new ConvertCsvRaportToList());

            var output = await testedUnit.GetRaport(new DateTime(2000, 05, 06), new DateTime(2000, 05, 06));

            Assert.Empty(output);
        }

        private IGetRaportFromExternalAPI GetUtilityForTest(IGetExternalApiBaseAddress getExternalApiBaseAddress, IConvertCsvRaportToList convertCsvRaportToList)
        {
            return new GetRaportFromExternalAPI(getExternalApiBaseAddress, convertCsvRaportToList);
        }
    }
}
