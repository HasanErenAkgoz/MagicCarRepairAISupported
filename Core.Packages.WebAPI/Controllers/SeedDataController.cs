using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Seeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedDataController : ControllerBase
    {
        private readonly BaseDbContext _context;
        private readonly ILogger<SeedDataController> _logger;

        public SeedDataController(BaseDbContext context, ILogger<SeedDataController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Seed data'yı manuel olarak çalıştırır (Development için)
        /// </summary>
        /// <param name="clearExisting">Mevcut verileri temizle</param>
        [HttpPost("run")]
        public async Task<IActionResult> RunSeedData([FromQuery] bool clearExisting = false)
        {
            try
            {
                _logger.LogInformation("Manual seed data process started (ClearExisting: {ClearExisting})...", clearExisting);

                if (clearExisting)
                {
                    await ClearAllTablesAsync();
                }

                // Seed Customers
                await SeedCustomersAsync();

                // Seed Vehicles
                await SeedVehiclesAsync();

                // Seed Work Orders
                await SeedWorkOrdersAsync();

                // Seed Incomes
                await SeedIncomesAsync();

                // Seed Expenses
                await SeedExpensesAsync();

                // Seed Parts
                await SeedPartsAsync();

                // Seed Part Stocks
                await SeedPartStocksAsync();

                _logger.LogInformation("Manual seed data process completed successfully.");

                return Ok(new { message = "Seed data completed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in manual seed data: {Message}", ex.Message);
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        private async Task SeedCustomersAsync()
        {
            var hasData = await _context.Set<Customer>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("Customers table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding customers...");
            var customers = CustomerSeedData.GetCustomers();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for Customers table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customers ON");
                
                await _context.Set<Customer>().AddRangeAsync(customers);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Customers OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} customers.", customers.Count);
        }

        private async Task SeedVehiclesAsync()
        {
            var hasData = await _context.Set<Vehicle>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("Vehicles table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding vehicles...");
            var vehicles = VehicleSeedData.GetVehicles();
            var kilometersMap = VehicleSeedData.GetVehicleKilometers();

            foreach (var vehicle in vehicles)
            {
                if (kilometersMap.TryGetValue(vehicle.Id, out var kilometers))
                {
                    vehicle.UpdateKilometers(kilometers);
                }
            }

            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for Vehicles table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Vehicles ON");
                
                await _context.Set<Vehicle>().AddRangeAsync(vehicles);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Vehicles OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} vehicles.", vehicles.Count);
        }

        private async Task SeedWorkOrdersAsync()
        {
            var hasData = await _context.Set<WorkOrder>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("WorkOrders table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding work orders...");
            var workOrders = WorkOrderSeedData.GetWorkOrders();

            // Set BaseEntity Status property for each work order
            foreach (var workOrder in workOrders)
            {
                var baseEntityType = typeof(BaseEntity<int>);
                var statusProperty = baseEntityType.GetProperty("Status", BindingFlags.Public | BindingFlags.Instance);
                if (statusProperty != null)
                {
                    statusProperty.SetValue(workOrder, Status.Active);
                }
            }

            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for WorkOrders table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT WorkOrders ON");
                
                await _context.Set<WorkOrder>().AddRangeAsync(workOrders);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT WorkOrders OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} work orders.", workOrders.Count);
        }

        private async Task SeedIncomesAsync()
        {
            var hasData = await _context.Set<Income>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("Incomes table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding incomes...");
            var incomes = IncomeSeedData.GetIncomes();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for Incomes table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Incomes ON");
                
                await _context.Set<Income>().AddRangeAsync(incomes);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Incomes OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} incomes.", incomes.Count);
        }

        private async Task SeedExpensesAsync()
        {
            var hasData = await _context.Set<Expense>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("Expenses table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding expenses...");
            var expenses = ExpenseSeedData.GetExpenses();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for Expenses table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Expenses ON");
                
                await _context.Set<Expense>().AddRangeAsync(expenses);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Expenses OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} expenses.", expenses.Count);
        }

        private async Task SeedPartsAsync()
        {
            var hasData = await _context.Set<Part>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("Parts table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding parts...");
            var parts = PartSeedData.GetParts();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for Parts table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Parts ON");
                
                await _context.Set<Part>().AddRangeAsync(parts);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Parts OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} parts.", parts.Count);
        }

        private async Task SeedPartStocksAsync()
        {
            var hasData = await _context.Set<PartStock>().AnyAsync();
            if (hasData)
            {
                _logger.LogInformation("PartStocks table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding part stocks...");
            var partStocks = PartStockSeedData.GetPartStocks();
            
            // Use transaction to ensure IDENTITY_INSERT is in the same transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Enable IDENTITY_INSERT for PartStocks table
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PartStocks ON");
                
                await _context.Set<PartStock>().AddRangeAsync(partStocks);
                await _context.SaveChangesAsync();
                
                // Disable IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT PartStocks OFF");
                
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
            _logger.LogInformation("Seeded {Count} part stocks.", partStocks.Count);
        }

        private async Task ClearAllTablesAsync()
        {
            _logger.LogInformation("Clearing all tables...");
            
            // Order is important because of foreign keys
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM PartStocks");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Parts");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Incomes");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Expenses");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM WorkOrderTimeline");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM WorkOrderPhotos");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM WorkOrderItems");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM WorkOrderLabors");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM WorkOrders");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Vehicles");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Customers");
            
            _logger.LogInformation("All tables cleared.");
        }
    }
}
