using Protel.Entity;
using System;

namespace Protel.Unit.Test.Builder
{
    public class CurrencyPriceBuilder
    {
        private CurrencyPrice _currencyPrice = new CurrencyPrice();
        public CurrencyPriceBuilder Id(int id)
        {
            _currencyPrice.Id = id;
            return this;
        }

        public CurrencyPriceBuilder Buy(double buy)
        {
            _currencyPrice.Buy = buy;
            return this;
        }
        public CurrencyPriceBuilder Sell(double sell)
        {
            _currencyPrice.Sell = sell;
            return this;
        }
        public CurrencyPriceBuilder Currency(Entity.Currency currency)
        {
            _currencyPrice.Currency = currency;
            _currencyPrice.CurrencyId = currency.Id;
            return this;
        }
        public CurrencyPriceBuilder CreatedAt(int day)
        {
            _currencyPrice.CreateDate = DateTime.Now.AddDays(-day);
            return this;
        }
        public CurrencyPrice Build() => _currencyPrice;
    }
}
