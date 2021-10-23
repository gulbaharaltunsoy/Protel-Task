using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using Protel.Context;
using Protel.Services;
using Protel.Unit.Test.Builder;
using Protel.Worker;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Protel.Unit.Test
{
    public class CurrencyServiceTest
    {
        private CurrencyService SetupService()
        {
            var data = new CurrencyBuilder().BuildList();
            var mockSet = data.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<IAppDbContext>();
            mockContext.Reset();
            mockContext.Setup(db => db.Currencies).Returns(mockSet.Object);

            var service = new CurrencyService(mockContext.Object);
            return service;
        }


        [Fact]
        public async Task GetCurrencies_Should_Be_Only_Currency_Code()
        {
            var service = SetupService();

            var currencies =await service.GetCurrencies();

            Assert.True(currencies.Any());
        }

        [Fact]
        public async Task GetCurrencyHistory_USD_Should_Be_Find_Currency_With_History()
        {
            var service = SetupService();

            var currency = await service.GetCurrencyHistory("USD");

            Assert.True(currency.Any());
            Assert.NotNull(currency);
        }

        [Fact]
        public async Task GetCurrencyHistory_AUD_Should_Be_Not_Find_Currency()
        {
            var service = SetupService();

            var currency = await service.GetCurrencyHistory("AUD");

            Assert.Null(currency);
        }

        [Fact]
        public async Task GetCurrencyHistory_Empty_Currency_Should_Be_Not_Find_Currency()
        {
            var service = SetupService();

            var currency = await service.GetCurrencyHistory("");

            Assert.Null(currency);
        }
    }
}
