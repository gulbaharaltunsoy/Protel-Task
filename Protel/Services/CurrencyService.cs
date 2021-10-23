using Microsoft.EntityFrameworkCore;
using Protel.Context;
using Protel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IAppDbContext _context;
        public CurrencyService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CodeRateViewModel>> GetCurrenciesWithRate(string sortField, string sortType)
        {
            var currencies = await _context.Currencies.Include(x => x.CurrencyPrices).ToListAsync();
            var query = currencies.Select(x => new CodeRateViewModel()
            {
                Code = $"TRY/{x.Code}",
                Buy = x.CurrencyPrices.Last().Buy,
                Sell = x.CurrencyPrices.Last().Sell,
            });

            if (sortField == "Code" && sortType == "ASC")
                query = query.OrderBy(x => x.Code);
            else if (sortField == "Code" && sortType == "DESC")
                query = query.OrderByDescending(x => x.Code);
            else if (sortField == "BUY" && sortType == "ASC")
                query = query.OrderBy(x => x.Buy);
            else if (sortField == "BUY" && sortType == "DESC")
                query = query.OrderByDescending(x => x.Buy);
            else if (sortField == "SELL" && sortType == "ASC")
                query = query.OrderBy(x => x.Sell);
            else if (sortField == "SELL" && sortType == "DESC")
                query = query.OrderByDescending(x => x.Sell);

            var result = query.ToList();
            return result;
        }

        public async Task<List<string>> GetCurrencies()
        {
            var currencies = await _context.Currencies.Select(x => x.Code).ToListAsync();
            return currencies;
        }

        public async Task<List<HistoryViewModel>> GetCurrencyHistory(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            var currencies = await _context.Currencies.Include(x => x.CurrencyPrices).FirstOrDefaultAsync(x => x.Code == code);
            if (currencies == null)
                return null;

            var query = currencies.CurrencyPrices.Select(x => new HistoryViewModel()
            {
                Code = $"TRY/{currencies.Code}",
                Buy = x.Buy,
                Sell = x.Sell,
                CreatedDate = x.CreateDate
            }).OrderBy(x => x.CreatedDate);

            var result = query.ToList();
            return result;
        }
    }
}
