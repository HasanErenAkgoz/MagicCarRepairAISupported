using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MagicCarRepairAISupported.Persistence.Seeds;

/// <summary>
/// Development / test ortamında boş tabloları demo verilerle doldurur (API startup veya POST /api/SeedData/run).
/// </summary>
public static class DevelopmentDemoDataSeedRunner
{
    private const string ManagerEmail = "manager@magiccar.com";
    private const string ManagerPassword = "Manager123!";

    public static bool IsAutoSeedEnabled(IConfiguration configuration, IHostEnvironment environment) =>
        configuration.GetValue("DatabaseSeed:AutoSeedDemoData", environment.IsDevelopment());

    public static async Task RunIfNeededAsync(
        IServiceProvider serviceProvider,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        if (!IsAutoSeedEnabled(configuration, environment))
        {
            logger.LogDebug("DatabaseSeed:AutoSeedDemoData is disabled. Skipping demo data seed.");
            return;
        }

        var context = serviceProvider.GetRequiredService<BaseDbContext>();
        if (!await context.Database.CanConnectAsync(cancellationToken))
            return;

        var hasCustomers = await context.Set<Customer>().AnyAsync(cancellationToken);
        if (!hasCustomers)
        {
            logger.LogInformation("Auto-seeding demo CRM data (customers, vehicles, work orders, parts, ...)...");
            await SeedCoreBusinessTablesAsync(context, logger, cancellationToken);
        }

        await EnsureAnchorCustomerAsync(context, logger, cancellationToken);
        await SeedErrorMessagesIfEmptyAsync(context, logger, cancellationToken);

        var needsExtended = !await context.Set<PartSupplier>().AnyAsync(cancellationToken);
        if (needsExtended)
        {
            logger.LogInformation("Auto-seeding extended demo data (suppliers, insurance, notifications, ...)...");
            await ExtendedDemoDataSeeder.SeedAsync(context, logger, cancellationToken);
        }

        await EnsureDevelopmentTestUsersAsync(serviceProvider, logger, cancellationToken);
        logger.LogInformation("Development demo data seed check completed.");
    }

    public static async Task SeedCoreBusinessTablesAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        await SeedCustomersAsync(context, logger, cancellationToken);
        await SeedVehiclesAsync(context, logger, cancellationToken);
        await SeedWorkOrdersAsync(context, logger, cancellationToken);
        await SeedIncomesAsync(context, logger, cancellationToken);
        await SeedExpensesAsync(context, logger, cancellationToken);
        await SeedPartsAsync(context, logger, cancellationToken);
        await SeedPartStocksAsync(context, logger, cancellationToken);
    }

    public static async Task EnsureAnchorCustomerAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken = default)
    {
        var exists = await context.Set<Customer>()
            .IgnoreQueryFilters()
            .AnyAsync(c => c.Id == 1 && c.Status != Status.Deleted, cancellationToken);
        if (exists)
            return;

        var anchor = CustomerSeedData.GetCustomers().FirstOrDefault(c => c.Id == 1);
        if (anchor == null)
            return;

        logger.LogInformation("Inserting anchor customer Id=1 (TestSprite / loyalty tests).");
        await InsertSeedRowsAsync(context, context.Set<Customer>(), [anchor], "Customers", cancellationToken);
    }

    private static async Task SeedErrorMessagesIfEmptyAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<ErrorMessage>().AnyAsync(cancellationToken))
            return;

        logger.LogInformation("Seeding error messages...");
        var messages = ErrorMessageSeedData.GetErrorMessages();
        await InsertSeedRowsAsync(context, context.Set<ErrorMessage>(), messages, "ErrorMessages", cancellationToken);
        logger.LogInformation("Seeded {Count} error messages.", messages.Count);
    }

    private static async Task EnsureDevelopmentTestUsersAsync(
        IServiceProvider serviceProvider,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        if (await userManager.FindByEmailAsync(ManagerEmail) != null)
            return;

        const string managerRoleName = "Manager";
        if (!await roleManager.RoleExistsAsync(managerRoleName))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = managerRoleName,
                NormalizedName = managerRoleName.ToUpperInvariant(),
                ClientId = 1,
            });
        }

        var manager = new User
        {
            FirstName = "Test",
            LastName = "Manager",
            Email = ManagerEmail,
            UserName = ManagerEmail,
            NormalizedEmail = ManagerEmail.ToUpperInvariant(),
            NormalizedUserName = ManagerEmail.ToUpperInvariant(),
            UserType = UserType.Manager,
            ClientId = 1,
            Language = "tr",
            EmailConfirmed = true,
            HasCompletedOnboarding = true,
        };

        var result = await userManager.CreateAsync(manager, ManagerPassword);
        if (!result.Succeeded)
        {
            logger.LogWarning(
                "Could not create development manager {Email}: {Errors}",
                ManagerEmail,
                string.Join(", ", result.Errors.Select(e => e.Description)));
            return;
        }

        await userManager.AddToRoleAsync(manager, managerRoleName);
        logger.LogInformation("Development manager created: {Email}", ManagerEmail);
    }

    private static async Task SeedCustomersAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<Customer>().AnyAsync(cancellationToken))
        {
            logger.LogInformation("Customers table already has data. Skipping seed.");
            return;
        }

        logger.LogInformation("Seeding customers...");
        var customers = CustomerSeedData.GetCustomers();
        await InsertSeedRowsAsync(context, context.Set<Customer>(), customers, "Customers", cancellationToken);
        logger.LogInformation("Seeded {Count} customers.", customers.Count);
    }

    private static async Task SeedVehiclesAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<Vehicle>().AnyAsync(cancellationToken))
        {
            logger.LogInformation("Vehicles table already has data. Skipping seed.");
            return;
        }

        logger.LogInformation("Seeding vehicles...");
        var vehicles = VehicleSeedData.GetVehicles();
        var kilometersMap = VehicleSeedData.GetVehicleKilometers();
        foreach (var vehicle in vehicles)
        {
            if (kilometersMap.TryGetValue(vehicle.Id, out var kilometers))
                vehicle.UpdateKilometers(kilometers);
        }

        await InsertSeedRowsAsync(context, context.Set<Vehicle>(), vehicles, "Vehicles", cancellationToken);
        logger.LogInformation("Seeded {Count} vehicles.", vehicles.Count);
    }

    private static async Task SeedWorkOrdersAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<WorkOrder>().AnyAsync(cancellationToken))
        {
            logger.LogInformation("WorkOrders table already has data. Skipping seed.");
            return;
        }

        logger.LogInformation("Seeding work orders...");
        var workOrders = WorkOrderSeedData.GetWorkOrders();
        foreach (var workOrder in workOrders)
        {
            var statusProperty = typeof(BaseEntity<int>).GetProperty(
                "Status",
                BindingFlags.Public | BindingFlags.Instance);
            statusProperty?.SetValue(workOrder, Status.Active);
        }

        await InsertSeedRowsAsync(context, context.Set<WorkOrder>(), workOrders, "WorkOrders", cancellationToken);
        logger.LogInformation("Seeded {Count} work orders.", workOrders.Count);
    }

    private static async Task SeedIncomesAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<Income>().AnyAsync(cancellationToken))
            return;

        logger.LogInformation("Seeding incomes...");
        var incomes = IncomeSeedData.GetIncomes();
        await InsertSeedRowsAsync(context, context.Set<Income>(), incomes, "Incomes", cancellationToken);
        logger.LogInformation("Seeded {Count} incomes.", incomes.Count);
    }

    private static async Task SeedExpensesAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<Expense>().AnyAsync(cancellationToken))
            return;

        logger.LogInformation("Seeding expenses...");
        var expenses = ExpenseSeedData.GetExpenses();
        await InsertSeedRowsAsync(context, context.Set<Expense>(), expenses, "Expenses", cancellationToken);
        logger.LogInformation("Seeded {Count} expenses.", expenses.Count);
    }

    private static async Task SeedPartsAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<Part>().AnyAsync(cancellationToken))
            return;

        logger.LogInformation("Seeding parts...");
        var parts = PartSeedData.GetParts();
        await InsertSeedRowsAsync(context, context.Set<Part>(), parts, "Parts", cancellationToken);
        logger.LogInformation("Seeded {Count} parts.", parts.Count);
    }

    private static async Task SeedPartStocksAsync(
        BaseDbContext context,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        if (await context.Set<PartStock>().AnyAsync(cancellationToken))
            return;

        logger.LogInformation("Seeding part stocks...");
        var partStocks = PartStockSeedData.GetPartStocks();
        await InsertSeedRowsAsync(context, context.Set<PartStock>(), partStocks, "PartStocks", cancellationToken);
        logger.LogInformation("Seeded {Count} part stocks.", partStocks.Count);
    }

    private static async Task InsertSeedRowsAsync<TEntity>(
        BaseDbContext context,
        DbSet<TEntity> dbSet,
        IList<TEntity> rows,
        string tableName,
        CancellationToken cancellationToken) where TEntity : class
    {
        await DatabaseSeedSqlHelper.ExecuteWithExplicitPrimaryKeysAsync(
            context,
            tableName,
            async ct =>
            {
                await dbSet.AddRangeAsync(rows, ct);
                await context.SaveChangesAsync(ct);
            },
            cancellationToken);
    }
}
