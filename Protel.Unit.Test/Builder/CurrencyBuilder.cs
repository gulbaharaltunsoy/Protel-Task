using Protel.Entity;
using System.Collections.Generic;
using System.Text;

namespace Protel.Unit.Test.Builder
{
    public class CurrencyBuilder
    {
        private Currency _currency = new Currency();

        public CurrencyBuilder Id(int id)
        {
            _currency.Id = id;
            return this;
        }

        public CurrencyBuilder Code(string code)
        {
            _currency.Code = code;
            return this;
        }
        public CurrencyBuilder Name(string name)
        {
            _currency.Name = name;
            return this;
        }
        public CurrencyBuilder TurkishName(string turkishName)
        {
            _currency.TurkishName = turkishName;
            return this;
        }
        public CurrencyBuilder AddPrices(int size)
        {
            for (int i = 1; i < size; i++)
            {
                _currency.CurrencyPrices.Add(new CurrencyPriceBuilder().Id(0).Buy(9.03 + i).Sell(9.04 * i).CreatedAt(i).Currency(_currency).Build());
            }

            return this;
        }
        public Currency Build() => _currency;
        public List<Currency> BuildList()
        {
            List<Currency> result = new List<Entity.Currency>()
            {
                new CurrencyBuilder().Id(0).Code("USD").Name("US DOLLAR").TurkishName("ABD DOLAR").AddPrices(5).Build(),
                new CurrencyBuilder().Id(0).Code("EUR").Name("EURO").TurkishName("EURO").AddPrices(3).Build(),
                new CurrencyBuilder().Id(0).Code("GBP").Name("POUND STERLING").TurkishName("İNGİLİZ STERLİNİ").AddPrices(4).Build(),
                new CurrencyBuilder().Id(0).Code("CHF").Name("SWISS FRANK").TurkishName("İSVİÇRE FRANGI").AddPrices(8).Build(),
                new CurrencyBuilder().Id(0).Code("KWD").Name("KUWAITI DINAR").TurkishName("KUVEYT DİNARI").AddPrices(2).Build(),
                new CurrencyBuilder().Id(0).Code("SAR").Name("SAUDI RIYAL").TurkishName("SUUDİ ARABİSTAN RİYALİ").AddPrices(1).Build(),
                new CurrencyBuilder().Id(0).Code("RUB").Name("RUSSIAN ROUBLE").TurkishName("RUS RUBLESİ").AddPrices(10).Build()
            };
            return result;
        }
    }
}
