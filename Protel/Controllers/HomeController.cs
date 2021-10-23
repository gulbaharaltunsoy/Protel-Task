using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Protel.Context;
using Protel.Models;
using Protel.Services;

namespace Protel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public HomeController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task<IActionResult> Index(string sortField, string sortType)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "Code";
            }
            if (string.IsNullOrEmpty(sortType))
            {
                sortField = "ASC";
            }
            var response =await _currencyService.GetCurrenciesWithRate(sortField, sortType);
            return View(response);
        }

        public async Task<IActionResult> History()
        {
            var response = await _currencyService.GetCurrencies();
            return View(response);
        }

        public async Task<IActionResult> CurrencyHistory(string code)
        {
            if (string.IsNullOrEmpty(code))
                return PartialView("_CurrencyHistory", null);
            
            var response = await _currencyService.GetCurrencyHistory(code);
            return PartialView("_CurrencyHistory", response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
