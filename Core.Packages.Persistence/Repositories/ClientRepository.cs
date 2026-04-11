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

        /// <summary>
        /// Cascade-deletes all data belonging to a client in FK-safe order.
        /// UserManager.DeleteAsync() for Identity users must be called separately BEFORE this method.
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
            await Context.WorkOrderPhotos.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrderItems.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrderLabors.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.WorkOrderTimelines.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Vehicle children ---
            await Context.VehiclePhotos.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: QuoteRequest children ---
            await Context.QuoteRequestPhotos.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.QuoteResponses.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Invoice children ---
            await Context.InvoiceItems.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Part / stock children ---
            await Context.PartPhotos.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.PartSuppliers.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.StockMovements.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.StockAlerts.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.AutoOrders.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Insurance children ---
            await Context.InsuranceClaims.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: Subscription children (table may not exist if migration pending) ---
            await TryDeleteAsync(() => Context.SubscriptionPayments.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "SubscriptionPayments");
            await Context.SalaryPayments.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 1: User-device / session data ---
            await Context.UserDeviceTokens.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.UserDevices.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.UserSessions.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 2: Parent entities ---
            await Context.WorkOrders.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Vehicles.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.QuoteRequests.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Invoices.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.PartStocks.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Parts.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.InsurancePolicies.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await TryDeleteAsync(() => Context.Subscriptions.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "Subscriptions");
            await Context.Employees.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Customers.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 3: Standalone client-level entities ---
            await Context.Appointments.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Payments.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Incomes.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Expenses.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Taxes.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await TryDeleteAsync(() => Context.Commissions.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "Commissions");
            await Context.ServiceRatings.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.ChatMessages.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Notifications.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.AuditLogs.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.LoyaltyPoints.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Rewards.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Reminders.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.HelpArticles.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.ServicePortfolios.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.Certificates.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await Context.FacilityPhotos.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);
            await TryDeleteAsync(() => Context.UsageTracking.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken), "UsageTracking");
            await Context.NotificationTemplates.Where(x => x.ClientId == clientId).ExecuteDeleteAsync(cancellationToken);

            // --- Level 4: Client itself ---
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

