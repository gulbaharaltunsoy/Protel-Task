using Protel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Protel.Services
{
    public interface ICurrencyService
    {
        Task<List<CodeRateViewModel>> GetCurrenciesWithRate(string sortField, string sortType);
        Task<List<string>> GetCurrencies();
        Task<List<HistoryViewModel>> GetCurrencyHistory(string code);
    }
}
