using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MagicCarRepairAISupported.Persistence.Startup.HostedServices
{
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
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();

                    // Test database connection
                    if (!await context.Database.CanConnectAsync(cancellationToken))
                    {
                        _logger.LogError("Failed to connect to database. Please check your connection string and ensure the database exists.");
                        return;
                    }

                    _logger.LogInformation("Starting database seed process...");

                    // Seed Customers
                    await SeedCustomersAsync(context, cancellationToken);

                    // Seed Vehicles
                    await SeedVehiclesAsync(context, cancellationToken);

                    // Seed Vehicle Photos
                    await SeedVehiclePhotosAsync(context, cancellationToken);

                    // Seed Work Orders
                    await SeedWorkOrdersAsync(context, cancellationToken);

                    // Seed Incomes
                    await SeedIncomesAsync(context, cancellationToken);

                    // Seed Expenses
                    await SeedExpensesAsync(context, cancellationToken);

                    // Seed Parts
                    await SeedPartsAsync(context, cancellationToken);

                    // Seed Part Stocks
                    await SeedPartStocksAsync(context, cancellationToken);

                    _logger.LogInformation("Database seed process completed successfully.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding database: {Message}", ex.Message);
                // Don't throw - allow application to start even if seeding fails
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task SeedCustomersAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Customer>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Customers table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding customers...");
            var customers = GetCustomerSeedData();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for Customers table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customers ON", cancellationToken);
                
                await context.Set<Customer>().AddRangeAsync(customers, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customers OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} customers.", customers.Count);
        }

        private async Task SeedVehiclesAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Vehicle>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Vehicles table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding vehicles...");
            var vehicles = GetVehicleSeedData();
            var kilometersMap = VehicleSeedData.GetVehicleKilometers();
            
            // Set kilometers for each vehicle using UpdateKilometers method
            foreach (var vehicle in vehicles)
            {
                if (kilometersMap.TryGetValue(vehicle.Id, out var kilometers))
                {
                    vehicle.UpdateKilometers(kilometers);
                }
            }
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for Vehicles table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Vehicles ON", cancellationToken);
                
                await context.Set<Vehicle>().AddRangeAsync(vehicles, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Vehicles OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} vehicles.", vehicles.Count);
        }

        private async Task SeedVehiclePhotosAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<VehiclePhoto>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("VehiclePhotos table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding vehicle photos...");
            var vehiclePhotos = VehiclePhotoSeedData.GetVehiclePhotos();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for VehiclePhotos table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT VehiclePhotos ON", cancellationToken);
                
                await context.Set<VehiclePhoto>().AddRangeAsync(vehiclePhotos, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT VehiclePhotos OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("VehiclePhotos seeded successfully. Count: {Count}", vehiclePhotos.Count);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task SeedWorkOrdersAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<WorkOrder>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("WorkOrders table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding work orders...");
            var workOrders = GetWorkOrderSeedData();
            
            // Set BaseEntity Status property for each work order (WorkOrder has 'new Status' that hides BaseEntity.Status)
            foreach (var workOrder in workOrders)
            {
                // Use reflection to set BaseEntity.Status property
                var baseEntityType = typeof(BaseEntity<int>);
                var statusProperty = baseEntityType.GetProperty("Status", BindingFlags.Public | BindingFlags.Instance);
                if (statusProperty != null)
                {
                    statusProperty.SetValue(workOrder, Status.Active);
                }
            }
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for WorkOrders table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT WorkOrders ON", cancellationToken);
                
                await context.Set<WorkOrder>().AddRangeAsync(workOrders, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT WorkOrders OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} work orders.", workOrders.Count);
        }

        private async Task SeedIncomesAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Income>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Incomes table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding incomes...");
            var incomes = GetIncomeSeedData();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for Incomes table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Incomes ON", cancellationToken);
                
                await context.Set<Income>().AddRangeAsync(incomes, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Incomes OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} incomes.", incomes.Count);
        }

        private async Task SeedExpensesAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Expense>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Expenses table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding expenses...");
            var expenses = GetExpenseSeedData();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for Expenses table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Expenses ON", cancellationToken);
                
                await context.Set<Expense>().AddRangeAsync(expenses, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Expenses OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} expenses.", expenses.Count);
        }

        private async Task SeedPartsAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Part>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Parts table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding parts...");
            var parts = GetPartSeedData();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for Parts table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Parts ON", cancellationToken);
                
                await context.Set<Part>().AddRangeAsync(parts, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Parts OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} parts.", parts.Count);
        }

        private async Task SeedPartStocksAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<PartStock>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("PartStocks table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding part stocks...");
            var partStocks = GetPartStockSeedData();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Enable IDENTITY_INSERT for PartStocks table
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PartStocks ON", cancellationToken);
                
                await context.Set<PartStock>().AddRangeAsync(partStocks, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                // Disable IDENTITY_INSERT
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PartStocks OFF", cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} part stocks.", partStocks.Count);
        }

        // Helper methods to get seed data from seed data classes
        private List<Customer> GetCustomerSeedData()
        {
            return CustomerSeedData.GetCustomers();
        }

        private List<Vehicle> GetVehicleSeedData()
        {
            return VehicleSeedData.GetVehicles();
        }

        private List<WorkOrder> GetWorkOrderSeedData()
        {
            return WorkOrderSeedData.GetWorkOrders();
        }

        private List<Income> GetIncomeSeedData()
        {
            return IncomeSeedData.GetIncomes();
        }

        private List<Expense> GetExpenseSeedData()
        {
            return ExpenseSeedData.GetExpenses();
        }

        private List<Part> GetPartSeedData()
        {
            return PartSeedData.GetParts();
        }

        private List<PartStock> GetPartStockSeedData()
        {
            return PartStockSeedData.GetPartStocks();
        }

        private List<VehiclePhoto> GetVehiclePhotoSeedData()
        {
            return VehiclePhotoSeedData.GetVehiclePhotos();
        }
    }
}
