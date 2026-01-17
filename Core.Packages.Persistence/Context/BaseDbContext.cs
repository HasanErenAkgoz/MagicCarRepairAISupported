using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Persistence.Context
{
    public class BaseDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>

    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        private readonly ITenantService? _tenantService;
        
        public BaseDbContext(DbContextOptions options, IHttpContextAccessor? httpContextAccessor = null, ITenantService? tenantService = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<ErrorMessage> ErrorMessages { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartStock> PartStocks { get; set; }
        public DbSet<PartSupplier> PartSuppliers { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderItem> WorkOrderItems { get; set; }
        public DbSet<WorkOrderLabor> WorkOrderLabors { get; set; }
        public DbSet<WorkOrderTimeline> WorkOrderTimelines { get; set; }
        public DbSet<WorkOrderPhoto> WorkOrderPhotos { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }
        public DbSet<QuoteResponse> QuoteResponses { get; set; }
        public DbSet<QuoteRequestPhoto> QuoteRequestPhotos { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<StockAlert> StockAlerts { get; set; }
        public DbSet<AutoOrder> AutoOrders { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<SalaryPayment> SalaryPayments { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }
        public DbSet<ServiceRating> ServiceRatings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ServicePortfolio> ServicePortfolios { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<FacilityPhoto> FacilityPhotos { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
            builder.Ignore<IdentityUserClaim<int>>();
            builder.Ignore<IdentityRoleClaim<int>>();
            builder.Ignore<IdentityUserToken<int>>();
            builder.Ignore<IdentityUserLogin<int>>();
            
            // Apply global query filter for multi-tenancy
            ApplyGlobalFilters(builder);
            
            // Seed data
            Extensions.ModelBuilderExtensions.SeedData(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            // Suppress PendingModelChangesWarning for seed data with dynamic values
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }
        
        private void ApplyGlobalFilters(ModelBuilder builder)
        {
            // Apply filter to all entities implementing IClientEntity
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(IClientEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(IClientEntity.ClientId));
                    
                    // Get current client id
                    var clientId = _tenantService?.GetCurrentClientId();
                    
                    if (clientId.HasValue)
                    {
                        var constantValue = Expression.Constant(clientId.Value);
                        var equals = Expression.Equal(property, constantValue);
                        var lambda = Expression.Lambda(equals, parameter);
                        
                        builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                    }
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity<int>>();
            var clientId = _tenantService?.GetCurrentClientId();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.Status = Status.Active;

                    var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        entry.Entity.CreatedBy = int.Parse(userId);
                    }
                    
                    // Set ClientId for multi-tenant entities
                    if (entry.Entity is IClientEntity clientEntity && clientId.HasValue)
                    {
                        clientEntity.ClientId = clientId.Value;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = DateTime.UtcNow;

                    var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        entry.Entity.ModifiedBy = int.Parse(userId);
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
