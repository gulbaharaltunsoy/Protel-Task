using Microsoft.EntityFrameworkCore;
using Protel.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Protel.Context
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<Currency> Currencies { get; }
        DbSet<CurrencyPrice> CurrencyPrices { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
