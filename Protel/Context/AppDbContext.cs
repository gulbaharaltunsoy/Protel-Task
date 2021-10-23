using Microsoft.EntityFrameworkCore;
using Protel.Context;
using Protel.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Protel.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyPrice> CurrencyPrices { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
