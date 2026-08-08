using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ClientRepository : EfEntityRepository<Client, BaseDbContext>, IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(BaseDbContext context, IUnitOfWork unitOfWork, ILogger<ClientRepository> logger) : base(context, unitOfWork)
        {
            _logger = logger;
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await Context.Clients.FindAsync(id);
        }

        public async Task<Client?> GetByIdForAuthAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Client?> GetByCodeAsync(string code)
        {
            return await Context.Clients
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<bool> IsCodeUniqueAsync(string code, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                return !await Context.Clients
                    .AnyAsync(c => c.Code == code && c.Id != excludeId.Value);
            }

            return !await Context.Clients.AnyAsync(c => c.Code == code);
        }

        /// <inheritdoc />
        public async Task PrepareForClientUserDeletionAsync(int clientId, IReadOnlyList<int> userIds, CancellationToken cancellationToken)
        {
            // ChatMessage: Restrict on SenderId / ReceiverId — must go before AspNetUsers delete.
            // IgnoreQueryFilters: IClientEntity global filter uses current tenant; target client may differ (e.g. system admin).
            if (userIds.Count > 0)
            {
                await Context.ChatMessages
                    .IgnoreQueryFilters()
                    .Where(m =>
                        m.ClientId == clientId
                        || userIds.Contains(m.SenderId)
                        || (m.ReceiverId != null && userIds.Contains(m.ReceiverId.Value)))
                    .ExecuteDeleteAsync(cancellationToken);
            }
            else
            {
                await Context.ChatMessages.IgnoreQueryFilters().Where(m => m.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            }

            // Reminder: Restrict on UserId
            if (userIds.Count > 0)
            {
                await Context.Reminders
                    .IgnoreQueryFilters()
                    .Where(r => r.ClientId == clientId || userIds.Contains(r.UserId))
                    .ExecuteDeleteAsync(cancellationToken);
            }
            else
            {
                await Context.Reminders.IgnoreQueryFilters().Where(r => r.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            }

            // Customer / Employee optional UserId — default FK is non-cascade; clear before Identity delete
            await Context.Customers
                .IgnoreQueryFilters()
                .Where(c => c.ClientId == clientId && c.UserId != null)
                .ExecuteUpdateAsync(s => s.SetProperty(c => c.UserId, (int?)null), cancellationToken);

            await Context.Employees
                .IgnoreQueryFilters()
                .Where(e => e.ClientId == clientId && e.UserId != null)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.UserId, (int?)null), cancellationToken);
        }

        /// <summary>
        /// Cascade-deletes all data belonging to a client in FK-safe order.
        /// Call <see cref="PrepareForClientUserDeletionAsync"/> then UserManager.DeleteAsync for each user, then this method.
        /// </summary>
        public async Task DeleteCascadeAsync(int clientId, IEnumerable<int> userIds, CancellationToken cancellationToken)
        {
            var userIdList = userIds.ToList();

            // --- Level 1: PasswordResetOtps (keyed by UserId, no ClientId) ---
            if (userIdList.Any())
                await Context.PasswordResetOtps
                    .Where(x => userIdList.Contains(x.UserId))
                    .ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: WorkOrder children ---
            await Context.WorkOrderPhotos.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrderItems.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrderLabors.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrderTimelines.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Vehicle children ---
            await Context.VehiclePhotos.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: QuoteRequest children ---
            await Context.QuoteRequestPhotos.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.QuoteResponses.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Invoice children ---
            await Context.InvoiceItems.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Part / stock children ---
            await Context.PartPhotos.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // Invoice.SupplierId -> PartSupplier is Restrict; clear before deleting suppliers (Parts use SetNull on supplier delete)
            await Context.Invoices.IgnoreQueryFilters()
                .Where(x => x.ClientId == clientId && x.SupplierId != null)
                .ExecuteUpdateAsync(s => s.SetProperty(i => i.SupplierId, (int?)null), cancellationToken);
            await Context.PartSuppliers.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.StockMovements.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.StockAlerts.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.AutoOrders.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Insurance children ---
            await Context.InsuranceClaims.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Subscription children (table may not exist if migration pending) ---
            await TryDeleteAsync(() => Context.SubscriptionPayments.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "SubscriptionPayments");
            await Context.SalaryPayments.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: User-device / session data ---
            await Context.UserDeviceTokens.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.UserDevices.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.UserSessions.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // ServicePortfolios.WorkOrderId -> WorkOrders (Restrict) — must delete before WorkOrders
            await Context.ServicePortfolios.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // ServiceRatings.WorkOrderId -> WorkOrders (Restrict) — must delete before WorkOrders
            await Context.ServiceRatings.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // Commissions.PaymentId -> Payments (Restrict)
            await TryDeleteAsync(() => Context.Commissions.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "Commissions");
            // Payments: WorkOrderId, InvoiceId, CustomerId -> parents (Restrict) — delete before WorkOrders / Invoices / Customers
            await Context.Payments.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // Incomes.WorkOrderId, CustomerId -> WorkOrders, Customers (Restrict)
            await Context.Incomes.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 2: Parent entities ---
            await Context.Invoices.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrders.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.InsurancePolicies.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Vehicles.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.QuoteRequests.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.PartStocks.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Parts.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // Policies reference InsuranceCompanies; companies reference Client — delete after policies
            await Context.InsuranceCompanies.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await TryDeleteAsync(() => Context.Subscriptions.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "Subscriptions");
            await Context.Employees.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // Appointments.CustomerId -> Customers (Restrict) — must delete before Customers
            await Context.Appointments.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            // LoyaltyPoints.CustomerId -> Customers (Restrict) — must delete before Customers
            await Context.LoyaltyPoints.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Customers.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 3: Standalone client-level entities ---
            await Context.Expenses.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Taxes.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.ChatMessages.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Notifications.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.AuditLogs.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Rewards.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Reminders.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.HelpArticles.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Certificates.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.FacilityPhotos.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await TryDeleteAsync(() => Context.UsageTracking.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "UsageTracking");
            await Context.NotificationTemplates.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // Roles.ClientId -> Clients (Restrict); RolePermissions.RoleId -> Roles (Restrict) — before Client delete
            await Context.RolePermissions.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Roles.IgnoreQueryFilters().Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 4: Client itself (no tenant filter on Client entity) ---
            await Context.Clients.Where(x => x.Id == clientId).ExecuteDeleteAsync(cancellationToken);
        }

        private async Task TryDeleteAsync(Func<Task> action, string tableName)
        {
            try
            {
                await action();
            }
            catch (Exception ex) when (ex.Message.Contains("Invalid object name") || ex.Message.Contains("does not exist"))
            {
                _logger.LogWarning("Tablo bulunamadı, atlanıyor: {Table}. Bekleyen migration uygulanmalı.", tableName);
            }
        }
    }
}

