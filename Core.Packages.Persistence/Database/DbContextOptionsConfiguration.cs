using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MagicCarRepairAISupported.Persistence.Database;

public static class DbContextOptionsConfiguration
{
    public static void UseMagicCarRepairDatabase(
        this DbContextOptionsBuilder options,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "ConnectionStrings:DefaultConnection is missing. Use PostgreSQL, e.g. Host=localhost;Port=5432;Database=MagicCarRepairDb;Username=magiccar;Password=magiccar_dev");
        }

        options.UseNpgsql(
            connectionString,
            npgsql => npgsql.MigrationsAssembly(typeof(BaseDbContext).Assembly.GetName().Name));
    }
}
