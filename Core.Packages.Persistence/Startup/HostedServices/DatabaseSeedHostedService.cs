using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Database;
using MagicCarRepairAISupported.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Persistence.Startup.HostedServices;

public class DatabaseSeedHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseSeedHostedService> _logger;

    public DatabaseSeedHostedService(IServiceProvider serviceProvider, ILogger<DatabaseSeedHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();

            if (!await context.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogError("Failed to connect to database. Check ConnectionStrings:DefaultConnection.");
                return;
            }

            if (context.Database.IsRelational())
            {
                _logger.LogInformation("Applying pending EF Core migrations...");
                await context.Database.MigrateAsync(cancellationToken);
                await EnsureQuoteRequestSqlServerSchemaAsync(context, cancellationToken);
            }

            _logger.LogInformation("Bootstrap seed: platform client + Super Admin (+ demo data when enabled).");

            await SeedClientsAsync(context, cancellationToken);
            await SeedSuperAdminAsync(scope.ServiceProvider, cancellationToken);

            await DevelopmentDemoDataSeedRunner.RunIfNeededAsync(
                scope.ServiceProvider,
                _logger,
                cancellationToken);

            _logger.LogInformation("Bootstrap seed completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding database: {Message}", ex.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// Legacy SQL Server patch; no-op on PostgreSQL.
    /// </summary>
    private async Task EnsureQuoteRequestSqlServerSchemaAsync(BaseDbContext context, CancellationToken cancellationToken)
    {
        if (context.Database.ProviderName?.Contains("SqlServer", StringComparison.OrdinalIgnoreCase) != true)
            return;

        const string addColumnsDdl = @"
IF OBJECT_ID(N'[dbo].[QuoteRequests]', N'U') IS NULL RETURN;
IF COL_LENGTH('dbo.QuoteRequests', 'Description') IS NULL
    ALTER TABLE [dbo].[QuoteRequests] ADD [Description] nvarchar(2000) NULL;
IF COL_LENGTH('dbo.QuoteRequests', 'EstimatedCost') IS NULL
    ALTER TABLE [dbo].[QuoteRequests] ADD [EstimatedCost] decimal(18,2) NULL;
IF COL_LENGTH('dbo.QuoteRequests', 'EstimatedDescription') IS NULL
    ALTER TABLE [dbo].[QuoteRequests] ADD [EstimatedDescription] nvarchar(max) NULL;
IF COL_LENGTH('dbo.QuoteRequests', 'PhotoPaths') IS NULL
BEGIN
    ALTER TABLE [dbo].[QuoteRequests] ADD [PhotoPaths] nvarchar(max) NOT NULL
        CONSTRAINT [DF_QuoteRequests_PhotoPaths_Startup] DEFAULT N'[]';
END";

        await context.Database.ExecuteSqlRawAsync(addColumnsDdl, cancellationToken);
    }

    private async Task SeedClientsAsync(BaseDbContext context, CancellationToken cancellationToken)
    {
        var templates = ClientSeedData.GetClients();
        var existingIds = await context.Set<Client>().Select(c => c.Id).ToListAsync(cancellationToken);
        var clientsToInsert = templates.Where(c => !existingIds.Contains(c.Id)).ToList();

        if (clientsToInsert.Count == 0)
        {
            _logger.LogInformation("Platform client (Id=1) already exists. Skipping.");
            return;
        }

        _logger.LogInformation("Seeding platform client...");

        await DatabaseSeedSqlHelper.ExecuteWithExplicitPrimaryKeysAsync(
            context,
            "Clients",
            async ct =>
            {
                await context.Set<Client>().AddRangeAsync(clientsToInsert, ct);
                await context.SaveChangesAsync(ct);
            },
            cancellationToken);
    }

    private async Task SeedSuperAdminAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var admin = UserSeedData.GetUsers().Single();

        var existing = await userManager.FindByEmailAsync(admin.Email!);
        if (existing != null)
        {
            _logger.LogInformation("Super Admin {Email} already exists. Skipping.", admin.Email);
            return;
        }

        admin.Id = default;
        var result = await userManager.CreateAsync(admin);
        if (result.Succeeded)
        {
            _logger.LogInformation("Super Admin created: {Email}", admin.Email);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Failed to create Super Admin {Email}: {Errors}", admin.Email, errors);
        }
    }
}
