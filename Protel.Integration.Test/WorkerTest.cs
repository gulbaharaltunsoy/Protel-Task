using Hangfire;
using Microsoft.EntityFrameworkCore;
using Protel.Worker;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Protel.Integration.Test
{
    public class WorkerTest : BaseContextTestFixture, IDisposable
    {
        private string[] _currencies = new string[]{
            "USD","EUR","GBP","CHF","KWD","SAR","RUB"
        };
        public WorkerTest()
        {
            base.InitDb();
        }
        public void Dispose()
        {
            base._context.Dispose();
        }
        [Fact]
        public async Task BackgroundJob_Work_Single_Should_Be_Only_Write_One()
        {
            CurrencyJob job = new CurrencyJob(base._context);
            job.Work(JobCancellationToken.Null, new DateTime(2021, 10, 20, 12, 30, 30));
            var currencies = await _context.Currencies.ToListAsync(); // "USD","EUR","GBP","CHF","KWD","SAR","RUB"
            foreach (var item in currencies)
            {
                var hasCurrency = _currencies.Contains(item.Code);
                Assert.True(hasCurrency);
            }
            Assert.Equal(_currencies.Length, currencies.Count);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task BackgroundJob_Work_By_Parameter_Should_Be_Write_Only_Once_Currencies(int count)
        {
            CurrencyJob job = new CurrencyJob(base._context);
            for (int i = 0; i < count; i++)
            {
                job.Work(JobCancellationToken.Null, new DateTime(2021, 10, 20, 12, 30, 30));
            }
            var currenciesCount = await _context.Currencies.CountAsync(); // "USD","EUR","GBP","CHF","KWD","SAR","RUB"

            Assert.Equal(currenciesCount, _currencies.Length);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task BackgroundJob_Work_By_Parameter_Should_Be_Write_One_More_Currency_Prices(int count)
        {
            CurrencyJob job = new CurrencyJob(base._context);
            for (int i = 0; i < count; i++)
            {
                job.Work(JobCancellationToken.Null, new DateTime(2021, 10, 20, 12, 30, 30));
            }
            var currencyHistory = await _context.CurrencyPrices.CountAsync();

            var actual = _currencies.Length * count;
            Assert.Equal(currencyHistory, actual);
        }

        [Fact]
        public async Task BackgroundJob_Not_Work_Time()
        {
            CurrencyJob job = new CurrencyJob(base._context);
            job.Work(JobCancellationToken.Null, new DateTime(2021, 10, 20, 20, 30, 30));

            var currencies = await _context.Currencies.CountAsync();
            var currencyHistory = await _context.CurrencyPrices.CountAsync();

            Assert.Equal(currencies, 0);
            Assert.Equal(currencyHistory, 0);
        }

        [Fact]
        public async Task BackgroundJob_Not_Work_Day()
        {
            CurrencyJob job = new CurrencyJob(base._context);
            job.Work(JobCancellationToken.Null, new DateTime(2021, 10, 17, 12, 30, 30));

            var currencies = await _context.Currencies.CountAsync();
            var currencyHistory = await _context.CurrencyPrices.CountAsync();

            Assert.Equal(currencies, 0);
            Assert.Equal(currencyHistory, 0);
        }
    }
}
