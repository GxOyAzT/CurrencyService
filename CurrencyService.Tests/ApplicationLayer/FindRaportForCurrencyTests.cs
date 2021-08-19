using CurrencyService.Application.Features.FindRaport;
using CurrencyService.Persistance.Context;
using CurrencyService.Persistance.Repositories.ExchangeRate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyService.Tests.ApplicationLayer
{
    public class FindRaportForCurrencyTests
    {
        [Fact]
        public async Task TestA()
        {
            var testUlitity = GetUtilityForTest(new ExchangeRateRepo(GetContext()));

            var output = await testUlitity.FindRaport("PLN", "EUR", new DateTime(2021, 8, 16), new DateTime(2021, 8, 16));

            Assert.Single(output);
            Assert.Equal(4.57m, output[0].Value);
            Assert.Equal(new DateTime(2021,8,16), output[0].Date);
        }

        [Fact]
        public async Task TestB()
        {
            var testUlitity = GetUtilityForTest(new ExchangeRateRepo(GetContext()));

            var output = await testUlitity.FindRaport("PLN", "EUR", new DateTime(2021, 8, 14), new DateTime(2021, 8, 15));

            Assert.Single(output);
            Assert.Equal(4.57m, output[0].Value);
            Assert.Equal(new DateTime(2021,8,13), output[0].Date);
        }

        [Fact]
        public async Task TestC()
        {
            var testUlitity = GetUtilityForTest(new ExchangeRateRepo(GetContext()));

            var output = await testUlitity.FindRaport("PLN", "EUR", new DateTime(2021, 8, 11), new DateTime(2021, 8, 13));

            Assert.Equal(3, output.Count);
            Assert.Equal(4.59m, output[0].Value);
            Assert.Equal(new DateTime(2021, 8, 11), output[0].Date);
            Assert.Equal(4.57m, output[2].Value);
            Assert.Equal(new DateTime(2021, 8, 13), output[2].Date);
        }

        private AppDbContext GetContext() => new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CurrencyProduction;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").Options);

        private IFindRaportForCurrency GetUtilityForTest(IExchangeRateRepo exchangeRateRepo)
        {
            return new FindRaportForCurrency(exchangeRateRepo);
        }
    }
}
