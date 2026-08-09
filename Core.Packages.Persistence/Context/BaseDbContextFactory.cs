using MagicCarRepairAISupported.Persistence.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MagicCarRepairAISupported.Persistence.Context
{
    public class BaseDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
    {
        // EF only needs provider metadata while scaffolding a migration; it must not
        // depend on the WebAPI working directory, appsettings files, or user secrets.
        private const string DesignTimeConnectionString =
            "Host=localhost;Port=5432;Database=MagicCarRepairDesignTime;Username=design_time";

        public BaseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            optionsBuilder.UseNpgsql(
                string.IsNullOrWhiteSpace(connectionString) ? DesignTimeConnectionString : connectionString,
                npgsql => npgsql.MigrationsAssembly(typeof(BaseDbContext).Assembly.GetName().Name));

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            return new BaseDbContext(optionsBuilder.Options, mockHttpContextAccessor);
        }
    }

    public class MockHttpContextAccessor : IHttpContextAccessor
    {
        public HttpContext? HttpContext { get; set; } = null;
    }
}
