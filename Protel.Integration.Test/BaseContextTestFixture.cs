using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Protel.Context;

namespace Protel.Integration.Test
{
    public class BaseContextTestFixture
    {
        protected IAppDbContext _context;

        protected DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            var services = new ServiceCollection();
            services.AddEntityFrameworkInMemoryDatabase();
            var serviceProvider = services.BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("TestDb").UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected void InitDb()
        {
            var options = CreateNewContextOptions();
            _context = new AppDbContext(options);
        }
    }
}
