using Hangfire;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Protel.Context;
using Protel.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Protel.Worker
{
    public class CurrencyJob
    {
        private static string[] currencies = new string[]{
            "USD","EUR","GBP","CHF","KWD","SAR","RUB"
        };
        private readonly IAppDbContext _context;
        public CurrencyJob(IAppDbContext context)
        {
            _context = context;
        }
        public void Work(IJobCancellationToken cancellationToken, DateTime now)
        {
            if (now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday)
                return;
            if (now.Hour >= 9 && now.Hour <= 17) //9:00---17:59 günceller
            {

                string exchangeRate = "http://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(exchangeRate);
                foreach (var code in currencies)
                {
                    var currency = _context.Currencies.FirstOrDefault(x => x.Code == code);
                    if (currency == null)
                    {
                        var currencyName = xmlDoc.SelectSingleNode($"Tarih_Date/Currency[@Kod='{code}']/CurrencyName").InnerXml;
                        var name = xmlDoc.SelectSingleNode($"Tarih_Date/Currency[@Kod='{code}']/Isim").InnerXml;
                        currency = new Currency
                        {
                            Code = code,
                            TurkishName = name,
                            Name = currencyName
                        };
                    }
                    var buy = xmlDoc.SelectSingleNode($"Tarih_Date/Currency[@Kod='{code}']/ForexBuying").InnerXml;
                    var sel = xmlDoc.SelectSingleNode($"Tarih_Date/Currency[@Kod='{code}']/ForexSelling").InnerXml;

                    var currencyPrice = new CurrencyPrice()
                    {
                        Buy = Convert.ToDouble(buy),
                        Sell = Convert.ToDouble(sel),
                        CreateDate = DateTime.Now
                    };
                    if (currency.Id == 0)
                    {
                        currency.CurrencyPrices.Add(currencyPrice);
                        _context.Currencies.Add(currency);
                    }
                    else
                    {
                        currencyPrice.CurrencyId = currency.Id;
                        _context.CurrencyPrices.Add(currencyPrice);
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}
