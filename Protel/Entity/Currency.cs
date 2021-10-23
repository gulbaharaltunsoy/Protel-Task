using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Protel.Entity
{
    public class Currency
    {   
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TurkishName { get; set; }

        public ICollection<CurrencyPrice> CurrencyPrices { get; set; }
        public Currency()
        {
            CurrencyPrices = new HashSet<CurrencyPrice>();
        }
    }
}
