using System;

namespace Protel.Entity
{
    public class CurrencyPrice
    {
        public int Id { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
        public DateTime CreateDate { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency{get;set;}
    }
}
