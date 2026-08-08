using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Database;

/// <summary>
/// Cross-database helpers for seeding rows with explicit primary key values.
/// </summary>
public static class DatabaseSeedSqlHelper
{
    public static bool IsSqlServer(string? providerName) =>
        providerName?.Contains("SqlServer", StringComparison.OrdinalIgnoreCase) == true;

    public static bool IsPostgres(string? providerName) =>
        providerName?.Contains("Npgsql", StringComparison.OrdinalIgnoreCase) == true;

    public static async Task ExecuteWithExplicitPrimaryKeysAsync(
        BaseDbContext context,
        string tableName,
        Func<CancellationToken, Task> seedBody,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await EnableExplicitPrimaryKeysAsync(context, tableName, cancellationToken);
                await seedBody(cancellationToken);
                await DisableExplicitPrimaryKeysAsync(context, tableName, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    private static async Task EnableExplicitPrimaryKeysAsync(
        BaseDbContext context,
        string tableName,
        CancellationToken cancellationToken)
    {
        var provider = context.Database.ProviderName;
        if (IsSqlServer(provider))
        {
            await context.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {tableName} ON",
                cancellationToken);
            return;
        }

        if (IsPostgres(provider))
        {
            await context.Database.ExecuteSqlRawAsync(
                "SET session_replication_role = replica",
                cancellationToken);
        }
    }

    private static async Task DisableExplicitPrimaryKeysAsync(
        BaseDbContext context,
        string tableName,
        CancellationToken cancellationToken)
    {
        var provider = context.Database.ProviderName;
        if (IsSqlServer(provider))
        {
            await context.Database.ExecuteSqlRawAsync(
                $"SET IDENTITY_INSERT {tableName} OFF",
                cancellationToken);
            return;
        }

        if (IsPostgres(provider))
        {
            await context.Database.ExecuteSqlRawAsync(
                "SET session_replication_role = DEFAULT",
                cancellationToken);

            await context.Database.ExecuteSqlRawAsync(
                $"""
                SELECT setval(
                    pg_get_serial_sequence('"{tableName}"', 'Id'),
                    COALESCE((SELECT MAX("Id") FROM "{tableName}"), 1),
                    true)
                """,
                cancellationToken);
        }
    }
}
