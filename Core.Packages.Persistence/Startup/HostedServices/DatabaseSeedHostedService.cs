using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;
using System;

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

                    if (context.Database.IsRelational())
                    {
                        _logger.LogInformation("Applying pending EF Core migrations...");
                        await context.Database.MigrateAsync(cancellationToken);
                        await EnsureQuoteRequestSqlServerSchemaAsync(context, cancellationToken);
                    }

                    _logger.LogInformation("Starting database seed process...");

                    // Foundation data (must be seeded before everything else)
                    await SeedClientsAsync(context, cancellationToken);
                    await SeedRolesAsync(context, cancellationToken);
                    await SeedPermissionsAsync(context, cancellationToken);
                    await SeedRolePermissionsAsync(context, cancellationToken);
                    await SeedUsersAsync(scope.ServiceProvider, cancellationToken);
                    await SeedEmployeesAsync(context, cancellationToken);

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

                    // Seed Translations
                    await SeedTranslationsAsync(context, cancellationToken);

                    // Seed Shop Profiles (Certificates & Facility Photos)
                    await SeedCertificatesAsync(context, cancellationToken);
                    await SeedFacilityPhotosAsync(context, cancellationToken);

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

        /// <summary>
        /// Eski / yarım kalmış veritabanlarında QuoteRequests sütunlarını idempotent ekler (geçmiş tablosu yanlış olsa bile).
        /// T-SQL; yalnızca SQL Server.
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

            const string copyProblemToDescription = @"
UPDATE [dbo].[QuoteRequests]
SET [Description] = [ProblemDescription]
WHERE ([Description] IS NULL OR LTRIM(RTRIM([Description])) = N'')
  AND [ProblemDescription] IS NOT NULL;
";

            await context.Database.ExecuteSqlRawAsync(addColumnsDdl, cancellationToken);

            var escapedCopy = copyProblemToDescription.Trim().Replace("'", "''");
            await context.Database.ExecuteSqlRawAsync($@"
IF OBJECT_ID(N'[dbo].[QuoteRequests]', N'U') IS NULL RETURN;
IF COL_LENGTH('dbo.QuoteRequests', 'ProblemDescription') IS NOT NULL
  AND COL_LENGTH('dbo.QuoteRequests', 'Description') IS NOT NULL
    EXEC sp_executesql N'{escapedCopy}';
", cancellationToken);
        }

        private async Task SeedClientsAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Client>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Clients table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding clients...");
            var clients = ClientSeedData.GetClients();

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Clients ON", cancellationToken);
                await context.Set<Client>().AddRangeAsync(clients, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Clients OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} clients.", clients.Count);
        }

        private async Task SeedRolesAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var allRoles = RolePermissionSeedData.GetDemoClientRoles()
                .Concat(RolePermissionSeedData.GetTestClientRoles())
                .ToList();

            var existingRoles = await context.Set<Role>()
                .Select(r => new { r.Id, r.NormalizedName, r.ClientId })
                .ToListAsync(cancellationToken);
            var existingKeys = existingRoles.Select(r => (r.NormalizedName, r.ClientId)).ToHashSet();
            var existingIds = existingRoles.Select(r => r.Id).ToHashSet();
            var rolesToInsert = allRoles
                .Where(r => !existingIds.Contains(r.Id) && !existingKeys.Contains((r.NormalizedName, r.ClientId)))
                .ToList();

            if (!rolesToInsert.Any())
            {
                _logger.LogInformation("All seed roles already present. Skipping.");
                return;
            }

            _logger.LogInformation("Seeding {Count} missing roles...", rolesToInsert.Count);

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles ON", cancellationToken);
                await context.Set<Role>().AddRangeAsync(rolesToInsert, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} roles.", rolesToInsert.Count);
        }

        private async Task SeedPermissionsAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Permission>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Permissions table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding permissions...");
            var permissions = RolePermissionSeedData.GetGlobalPermissions();

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Permissions ON", cancellationToken);
                await context.Set<Permission>().AddRangeAsync(permissions, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Permissions OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} permissions.", permissions.Count);
        }

        private async Task SeedRolePermissionsAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<RolePermission>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("RolePermissions table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding role permissions...");
            var rolePermissions = RolePermissionSeedData.GetDemoClientRolePermissions()
                .Concat(RolePermissionSeedData.GetTestClientRolePermissions())
                .ToList();

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RolePermissions ON", cancellationToken);
                await context.Set<RolePermission>().AddRangeAsync(rolePermissions, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RolePermissions OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} role permissions.", rolePermissions.Count);
        }

        private async Task SeedUsersAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceProvider.GetRequiredService<BaseDbContext>();

            var hasData = await context.Set<User>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Users table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding users...");
            var users = UserSeedData.GetUsers();

            foreach (var user in users)
            {
                var result = await userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogError("Failed to create user {Email}: {Errors}", user.Email, errors);
                }
            }

            _logger.LogInformation("Seeded {Count} users.", users.Count);
        }

        private async Task SeedEmployeesAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Employee>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Employees table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding employees...");
            var employees = EmployeeSeedData.GetEmployees();

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Employees ON", cancellationToken);
                await context.Set<Employee>().AddRangeAsync(employees, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Employees OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} employees.", employees.Count);
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

        private async Task SeedTranslationsAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Translation>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Translations table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding translations...");

            try
            {
                var translations = GetTranslationSeedData();
                await context.Set<Translation>().AddRangeAsync(translations, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Seeded {Count} translations.", translations.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding translations: {Message}", ex.Message);
                // Don't throw - allow application to start even if translation seeding fails
            }
        }

        private List<Translation> GetTranslationSeedData()
        {
            var translations = new List<Translation>();
            var now = DateTime.UtcNow;
            const int systemUserId = 0; // System user for seed data

            // DomainException Messages - Turkish
            translations.AddRange(new[]
            {
                new Translation { Key = "DomainException.VEHICLE_KM_LOWER_THAN_CURRENT", Language = "tr", Value = "Yeni kilometre ({NewKilometers}), mevcut kilometreden ({CurrentKilometers}) düşük olamaz. Plaka: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_KM_NEGATIVE", Language = "tr", Value = "Kilometre değeri negatif olamaz. Girilen değer: {NewKilometers}. Plaka: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.USER_EMAIL_EXISTS", Language = "tr", Value = "Bu e-posta adresi zaten kullanılıyor: {Email}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.USER_AGE_INVALID", Language = "tr", Value = "Kullanıcı yaşı 18'den küçük olamaz. Mevcut yaş: {Age}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.PERMISSION_NOT_FOUND", Language = "tr", Value = "İzin bulunamadı: {PermissionName}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_NOT_FOUND", Language = "tr", Value = "Araç bulunamadı. ID: {VehicleId}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.CUSTOMER_NOT_FOUND", Language = "tr", Value = "Müşteri bulunamadı. ID: {CustomerId}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.INVALID_LICENSE_PLATE", Language = "tr", Value = "Geçersiz plaka formatı: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_ALREADY_EXISTS", Language = "tr", Value = "Bu plaka zaten kayıtlı: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // DomainException Messages - English
            translations.AddRange(new[]
            {
                new Translation { Key = "DomainException.VEHICLE_KM_LOWER_THAN_CURRENT", Language = "en", Value = "New kilometers ({NewKilometers}) cannot be lower than current kilometers ({CurrentKilometers}). License Plate: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_KM_NEGATIVE", Language = "en", Value = "Kilometers value cannot be negative. Entered value: {NewKilometers}. License Plate: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.USER_EMAIL_EXISTS", Language = "en", Value = "This email address is already in use: {Email}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.USER_AGE_INVALID", Language = "en", Value = "User age cannot be less than 18. Current age: {Age}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.PERMISSION_NOT_FOUND", Language = "en", Value = "Permission not found: {PermissionName}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_NOT_FOUND", Language = "en", Value = "Vehicle not found. ID: {VehicleId}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.CUSTOMER_NOT_FOUND", Language = "en", Value = "Customer not found. ID: {CustomerId}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.INVALID_LICENSE_PLATE", Language = "en", Value = "Invalid license plate format: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_ALREADY_EXISTS", Language = "en", Value = "This license plate is already registered: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // DomainException Messages - German
            translations.AddRange(new[]
            {
                new Translation { Key = "DomainException.VEHICLE_KM_LOWER_THAN_CURRENT", Language = "de", Value = "Neue Kilometer ({NewKilometers}) können nicht niedriger sein als aktuelle Kilometer ({CurrentKilometers}). Kennzeichen: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_KM_NEGATIVE", Language = "de", Value = "Kilometerwert kann nicht negativ sein. Eingegebener Wert: {NewKilometers}. Kennzeichen: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.USER_EMAIL_EXISTS", Language = "de", Value = "Diese E-Mail-Adresse wird bereits verwendet: {Email}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.USER_AGE_INVALID", Language = "de", Value = "Benutzeralter kann nicht unter 18 sein. Aktuelles Alter: {Age}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.PERMISSION_NOT_FOUND", Language = "de", Value = "Berechtigung nicht gefunden: {PermissionName}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_NOT_FOUND", Language = "de", Value = "Fahrzeug nicht gefunden. ID: {VehicleId}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.CUSTOMER_NOT_FOUND", Language = "de", Value = "Kunde nicht gefunden. ID: {CustomerId}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.INVALID_LICENSE_PLATE", Language = "de", Value = "Ungültiges Kennzeichenformat: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "DomainException.VEHICLE_ALREADY_EXISTS", Language = "de", Value = "Dieses Kennzeichen ist bereits registriert: {LicensePlate}", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // General Exception Messages - Turkish
            translations.AddRange(new[]
            {
                new Translation { Key = "Exception.ValidationException", Language = "tr", Value = "Doğrulama hatası oluştu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.UnauthorizedAccessException", Language = "tr", Value = "Bu işlem için yetkiniz bulunmamaktadır", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.NotFoundException", Language = "tr", Value = "Aranan kayıt bulunamadı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.ArgumentNullException", Language = "tr", Value = "Gerekli parametre eksik", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.ArgumentException", Language = "tr", Value = "Geçersiz parametre değeri", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.TimeoutException", Language = "tr", Value = "İşlem zaman aşımına uğradı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.SqlException", Language = "tr", Value = "Veritabanı hatası oluştu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.HttpRequestException", Language = "tr", Value = "Ağ bağlantı hatası", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // General Exception Messages - English
            translations.AddRange(new[]
            {
                new Translation { Key = "Exception.ValidationException", Language = "en", Value = "Validation error occurred", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.UnauthorizedAccessException", Language = "en", Value = "You do not have permission for this operation", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.NotFoundException", Language = "en", Value = "The requested record was not found", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.ArgumentNullException", Language = "en", Value = "Required parameter is missing", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.ArgumentException", Language = "en", Value = "Invalid parameter value", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.TimeoutException", Language = "en", Value = "Operation timed out", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.SqlException", Language = "en", Value = "Database error occurred", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.HttpRequestException", Language = "en", Value = "Network connection error", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // General Exception Messages - German
            translations.AddRange(new[]
            {
                new Translation { Key = "Exception.ValidationException", Language = "de", Value = "Validierungsfehler aufgetreten", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.UnauthorizedAccessException", Language = "de", Value = "Sie haben keine Berechtigung für diese Operation", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.NotFoundException", Language = "de", Value = "Der angeforderte Datensatz wurde nicht gefunden", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.ArgumentNullException", Language = "de", Value = "Erforderlicher Parameter fehlt", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.ArgumentException", Language = "de", Value = "Ungültiger Parameterwert", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.TimeoutException", Language = "de", Value = "Vorgang ist abgelaufen", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.SqlException", Language = "de", Value = "Datenbankfehler aufgetreten", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Exception.HttpRequestException", Language = "de", Value = "Netzwerkverbindungsfehler", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Success Messages - Turkish
            translations.AddRange(new[]
            {
                new Translation { Key = "Success.VehicleUpdated", Language = "tr", Value = "Araç bilgileri başarıyla güncellendi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.VehicleCreated", Language = "tr", Value = "Araç başarıyla oluşturuldu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.VehicleDeleted", Language = "tr", Value = "Araç başarıyla silindi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserCreated", Language = "tr", Value = "Kullanıcı başarıyla oluşturuldu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserUpdated", Language = "tr", Value = "Kullanıcı bilgileri başarıyla güncellendi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserDeleted", Language = "tr", Value = "Kullanıcı başarıyla silindi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerCreated", Language = "tr", Value = "Müşteri başarıyla oluşturuldu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerUpdated", Language = "tr", Value = "Müşteri bilgileri başarıyla güncellendi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerDeleted", Language = "tr", Value = "Müşteri başarıyla silindi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.PermissionAssigned", Language = "tr", Value = "İzin başarıyla atandı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleCreated", Language = "tr", Value = "Rol başarıyla oluşturuldu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleUpdated", Language = "tr", Value = "Rol başarıyla güncellendi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleDeleted", Language = "tr", Value = "Rol başarıyla silindi", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Success Messages - English
            translations.AddRange(new[]
            {
                new Translation { Key = "Success.VehicleUpdated", Language = "en", Value = "Vehicle information updated successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.VehicleCreated", Language = "en", Value = "Vehicle created successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.VehicleDeleted", Language = "en", Value = "Vehicle deleted successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserCreated", Language = "en", Value = "User created successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserUpdated", Language = "en", Value = "User information updated successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserDeleted", Language = "en", Value = "User deleted successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerCreated", Language = "en", Value = "Customer created successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerUpdated", Language = "en", Value = "Customer information updated successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerDeleted", Language = "en", Value = "Customer deleted successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.PermissionAssigned", Language = "en", Value = "Permission assigned successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleCreated", Language = "en", Value = "Role created successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleUpdated", Language = "en", Value = "Role updated successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleDeleted", Language = "en", Value = "Role deleted successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Success Messages - German
            translations.AddRange(new[]
            {
                new Translation { Key = "Success.VehicleUpdated", Language = "de", Value = "Fahrzeuginformationen erfolgreich aktualisiert", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.VehicleCreated", Language = "de", Value = "Fahrzeug erfolgreich erstellt", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.VehicleDeleted", Language = "de", Value = "Fahrzeug erfolgreich gelöscht", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserCreated", Language = "de", Value = "Benutzer erfolgreich erstellt", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserUpdated", Language = "de", Value = "Benutzerinformationen erfolgreich aktualisiert", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.UserDeleted", Language = "de", Value = "Benutzer erfolgreich gelöscht", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerCreated", Language = "de", Value = "Kunde erfolgreich erstellt", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerUpdated", Language = "de", Value = "Kundeninformationen erfolgreich aktualisiert", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.CustomerDeleted", Language = "de", Value = "Kunde erfolgreich gelöscht", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.PermissionAssigned", Language = "de", Value = "Berechtigung erfolgreich zugewiesen", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleCreated", Language = "de", Value = "Rolle erfolgreich erstellt", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleUpdated", Language = "de", Value = "Rolle erfolgreich aktualisiert", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Success.RoleDeleted", Language = "de", Value = "Rolle erfolgreich gelöscht", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Business Logic Messages - Turkish
            translations.AddRange(new[]
            {
                new Translation { Key = "Business.VehicleInUse", Language = "tr", Value = "Araç şu anda kullanımda olduğu için silinemez", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.CustomerHasVehicles", Language = "tr", Value = "Müşterinin kayıtlı araçları olduğu için silinemez", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.InvalidEmailFormat", Language = "tr", Value = "Geçersiz e-posta formatı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.PasswordTooWeak", Language = "tr", Value = "Şifre çok zayıf. En az 8 karakter, büyük harf, küçük harf, rakam ve özel karakter içermelidir", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.PhoneNumberInvalid", Language = "tr", Value = "Geçersiz telefon numarası formatı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.IdentityNumberInvalid", Language = "tr", Value = "Geçersiz kimlik numarası formatı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.LicensePlateInvalid", Language = "tr", Value = "Geçersiz plaka formatı. Örnek: 34ABC123", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.VehicleYearInvalid", Language = "tr", Value = "Geçersiz araç yılı. Yıl 1900 ile 2025 arasında olmalıdır", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Business Logic Messages - English
            translations.AddRange(new[]
            {
                new Translation { Key = "Business.VehicleInUse", Language = "en", Value = "Vehicle cannot be deleted as it is currently in use", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.CustomerHasVehicles", Language = "en", Value = "Customer cannot be deleted as they have registered vehicles", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.InvalidEmailFormat", Language = "en", Value = "Invalid email format", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.PasswordTooWeak", Language = "en", Value = "Password is too weak. Must contain at least 8 characters, uppercase, lowercase, number and special character", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.PhoneNumberInvalid", Language = "en", Value = "Invalid phone number format", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.IdentityNumberInvalid", Language = "en", Value = "Invalid identity number format", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.LicensePlateInvalid", Language = "en", Value = "Invalid license plate format. Example: 34ABC123", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.VehicleYearInvalid", Language = "en", Value = "Invalid vehicle year. Year must be between 1900 and 2025", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Business Logic Messages - German
            translations.AddRange(new[]
            {
                new Translation { Key = "Business.VehicleInUse", Language = "de", Value = "Fahrzeug kann nicht gelöscht werden, da es derzeit in Gebrauch ist", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.CustomerHasVehicles", Language = "de", Value = "Kunde kann nicht gelöscht werden, da er registrierte Fahrzeuge hat", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.InvalidEmailFormat", Language = "de", Value = "Ungültiges E-Mail-Format", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.PasswordTooWeak", Language = "de", Value = "Passwort ist zu schwach. Muss mindestens 8 Zeichen, Großbuchstaben, Kleinbuchstaben, Zahlen und Sonderzeichen enthalten", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.PhoneNumberInvalid", Language = "de", Value = "Ungültiges Telefonnummernformat", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.IdentityNumberInvalid", Language = "de", Value = "Ungültiges Ausweisnummernformat", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.LicensePlateInvalid", Language = "de", Value = "Ungültiges Kennzeichenformat. Beispiel: 34ABC123", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Business.VehicleYearInvalid", Language = "de", Value = "Ungültiges Fahrzeugjahr. Jahr muss zwischen 1900 und 2025 liegen", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // API Response Messages - Turkish
            translations.AddRange(new[]
            {
                new Translation { Key = "Api.Success", Language = "tr", Value = "İşlem başarıyla tamamlandı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Error", Language = "tr", Value = "İşlem sırasında hata oluştu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.NotFound", Language = "tr", Value = "Aranan kayıt bulunamadı", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Unauthorized", Language = "tr", Value = "Bu işlem için yetkiniz bulunmamaktadır", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Forbidden", Language = "tr", Value = "Bu işlemi gerçekleştirme yetkiniz bulunmamaktadır", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.BadRequest", Language = "tr", Value = "Geçersiz istek", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.InternalServerError", Language = "tr", Value = "Sunucu hatası oluştu", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.ValidationError", Language = "tr", Value = "Doğrulama hatası", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // API Response Messages - English
            translations.AddRange(new[]
            {
                new Translation { Key = "Api.Success", Language = "en", Value = "Operation completed successfully", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Error", Language = "en", Value = "An error occurred during the operation", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.NotFound", Language = "en", Value = "The requested record was not found", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Unauthorized", Language = "en", Value = "You do not have permission for this operation", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Forbidden", Language = "en", Value = "You do not have permission to perform this operation", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.BadRequest", Language = "en", Value = "Invalid request", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.InternalServerError", Language = "en", Value = "Server error occurred", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.ValidationError", Language = "en", Value = "Validation error", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // API Response Messages - German
            translations.AddRange(new[]
            {
                new Translation { Key = "Api.Success", Language = "de", Value = "Vorgang erfolgreich abgeschlossen", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Error", Language = "de", Value = "Während des Vorgangs ist ein Fehler aufgetreten", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.NotFound", Language = "de", Value = "Der angeforderte Datensatz wurde nicht gefunden", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Unauthorized", Language = "de", Value = "Sie haben keine Berechtigung für diesen Vorgang", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.Forbidden", Language = "de", Value = "Sie haben keine Berechtigung, diesen Vorgang auszuführen", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.BadRequest", Language = "de", Value = "Ungültige Anfrage", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.InternalServerError", Language = "de", Value = "Serverfehler aufgetreten", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Api.ValidationError", Language = "de", Value = "Validierungsfehler", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            // Test Messages - Turkish, English, German
            translations.AddRange(new[]
            {
                new Translation { Key = "Test.Welcome", Language = "tr", Value = "Hoş geldiniz!", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Test.Welcome", Language = "en", Value = "Welcome!", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Test.Welcome", Language = "de", Value = "Willkommen!", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Test.Goodbye", Language = "tr", Value = "Güle güle!", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Test.Goodbye", Language = "en", Value = "Goodbye!", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
                new Translation { Key = "Test.Goodbye", Language = "de", Value = "Auf Wiedersehen!", Status = Status.Active, CreatedDate = now, CreatedBy = systemUserId },
            });

            return translations;
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

        private async Task SeedCertificatesAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<Certificate>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("Certificates table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding certificates...");
            var certificates = ShopProfileSeedData.GetCertificates();

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Certificates ON", cancellationToken);
                await context.Set<Certificate>().AddRangeAsync(certificates, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Certificates OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} certificates.", certificates.Count);
        }

        private async Task SeedFacilityPhotosAsync(BaseDbContext context, CancellationToken cancellationToken)
        {
            var hasData = await context.Set<FacilityPhoto>().AnyAsync(cancellationToken);
            if (hasData)
            {
                _logger.LogInformation("FacilityPhotos table already has data. Skipping seed.");
                return;
            }

            _logger.LogInformation("Seeding facility photos...");
            var photos = ShopProfileSeedData.GetFacilityPhotos();

            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT FacilityPhotos ON", cancellationToken);
                await context.Set<FacilityPhoto>().AddRangeAsync(photos, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT FacilityPhotos OFF", cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            _logger.LogInformation("Seeded {Count} facility photos.", photos.Count);
        }
    }
}
