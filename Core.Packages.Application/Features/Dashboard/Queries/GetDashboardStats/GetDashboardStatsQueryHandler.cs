using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetDashboardStats
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, GetDashboardStatsResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPartRepository _partRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly IEntityRepository<StockAlert, int> _stockAlertRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly IEntityRepository<Vehicle, int> _vehicleRepository;
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IQuoteResponseRepository _quoteResponseRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ITenantService _tenantService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDashboardStatsQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            IInvoiceRepository invoiceRepository,
            IPartRepository partRepository,
            IPartStockRepository partStockRepository,
            IEntityRepository<StockAlert, int> stockAlertRepository,
            IEntityRepository<Customer, int> customerRepository,
            IEntityRepository<Vehicle, int> vehicleRepository,
            IQuoteRequestRepository quoteRequestRepository,
            IQuoteResponseRepository quoteResponseRepository,
            INotificationRepository notificationRepository,
            ITenantService tenantService,
            UserManager<UserEntity> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _invoiceRepository = invoiceRepository;
            _partRepository = partRepository;
            _partStockRepository = partStockRepository;
            _stockAlertRepository = stockAlertRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _quoteRequestRepository = quoteRequestRepository;
            _quoteResponseRepository = quoteResponseRepository;
            _notificationRepository = notificationRepository;
            _tenantService = tenantService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetDashboardStatsResponse> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            // SystemAdmin tüm clientları görebilir (clientId = null), Manager/Employee sadece kendi clientını
            int? clientId = null;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var userIdInt))
            {
                var user = await _userManager.FindByIdAsync(userIdInt.ToString());
                if (user != null && user.UserType != UserType.SystemAdmin)
                {
                    clientId = _tenantService.GetCurrentClientId() ?? user.ClientId;
                }
            }
            else
            {
                // Claim yoksa tenant servisine geri dön
                clientId = _tenantService.GetCurrentClientId();
            }

            var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-1);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            var response = new GetDashboardStatsResponse
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // İş Emirleri İstatistikleri (optimize: AsNoTracking kullan)
            var workOrders = _workOrderRepository.Query()
                .AsNoTracking()
                .Where(wo => !clientId.HasValue || wo.ClientId == clientId.Value);

            response.TotalWorkOrders = await workOrders.CountAsync(cancellationToken);
            response.ActiveWorkOrders = await workOrders
                .Where(wo => wo.Status != WorkOrderStatus.Delivered && wo.Status != WorkOrderStatus.Cancelled)
                .CountAsync(cancellationToken);
            response.CompletedWorkOrders = await workOrders
                .Where(wo => wo.Status == WorkOrderStatus.Delivered)
                .CountAsync(cancellationToken);
            response.PendingWorkOrders = await workOrders
                .Where(wo => wo.Status == WorkOrderStatus.VehicleEntered || wo.Status == WorkOrderStatus.AppointmentScheduled)
                .CountAsync(cancellationToken);

            // Finansal İstatistikler (optimize: AsNoTracking kullan)
            var incomes = _incomeRepository.Query()
                .AsNoTracking()
                .Where(i => (!clientId.HasValue || i.ClientId == clientId.Value) && i.TransactionDate >= startDate && i.TransactionDate <= endDate);
            response.TotalIncome = await incomes.SumAsync(i => (decimal?)i.Amount, cancellationToken) ?? 0;

            var expenses = _expenseRepository.Query()
                .AsNoTracking()
                .Where(e => (!clientId.HasValue || e.ClientId == clientId.Value) && e.TransactionDate >= startDate && e.TransactionDate <= endDate);
            response.TotalExpense = await expenses.SumAsync(e => (decimal?)e.Amount, cancellationToken) ?? 0;

            response.NetProfit = response.TotalIncome - response.TotalExpense;

            // Fatura İstatistikleri (optimize: AsNoTracking ve Select projection kullan)
            var pendingInvoices = await _invoiceRepository.Query()
                .AsNoTracking()
                .Where(i => (!clientId.HasValue || i.ClientId == clientId.Value) &&
                           (i.Status == InvoiceStatus.Pending || i.Status == InvoiceStatus.PartiallyPaid))
                .Select(i => new { i.TotalAmount, i.PaidAmount })
                .ToListAsync(cancellationToken);
            response.PendingInvoiceAmount = pendingInvoices.Sum(i => i.TotalAmount - i.PaidAmount);

            var overdueInvoices = await _invoiceRepository.GetOverdueInvoicesAsync(cancellationToken);
            response.OverdueInvoiceAmount = overdueInvoices
                .Where(i => !clientId.HasValue || i.ClientId == clientId.Value)
                .Sum(i => i.TotalAmount - i.PaidAmount);

            // Stok İstatistikleri (optimize: Select projection kullan, gereksiz veri çekme)
            var parts = await _partRepository.Query()
                .AsNoTracking()
                .Where(p => !clientId.HasValue || p.ClientId == clientId.Value)
                .Select(p => new { p.Id, p.MinimumStockLevel, p.IsLowStockAlertEnabled })
                .ToListAsync(cancellationToken);
            response.TotalParts = parts.Count;

            var partIds = parts.Select(p => p.Id).ToList();
            var partStocks = await _partStockRepository.Query()
                .AsNoTracking()
                .Where(ps => partIds.Contains(ps.PartId))
                .Select(ps => new { ps.PartId, ps.Quantity })
                .ToListAsync(cancellationToken);

            // In-memory join yerine database query kullan (daha performanslı)
            var lowStockPartsQuery = _partRepository.Query()
                .AsNoTracking()
                .Where(p => (!clientId.HasValue || p.ClientId == clientId.Value) &&
                           p.IsLowStockAlertEnabled &&
                           p.Status != Status.Deleted)
                .Join(_partStockRepository.Query().AsNoTracking(),
                      p => p.Id,
                      ps => ps.PartId,
                      (p, ps) => new { Part = p, Stock = ps })
                .Where(x => x.Stock.Quantity <= x.Part.MinimumStockLevel &&
                           x.Stock.Quantity > 0);
            response.LowStockParts = await lowStockPartsQuery.CountAsync(cancellationToken);

            var outOfStockPartsQuery = _partRepository.Query()
                .AsNoTracking()
                .Where(p => (!clientId.HasValue || p.ClientId == clientId.Value) &&
                           p.Status != Status.Deleted)
                .GroupJoin(_partStockRepository.Query().AsNoTracking(),
                          p => p.Id,
                          ps => ps.PartId,
                          (p, stocks) => new { Part = p, Stock = stocks.FirstOrDefault() })
                .Where(x => x.Stock == null || x.Stock.Quantity == 0);
            response.OutOfStockParts = await outOfStockPartsQuery.CountAsync(cancellationToken);

            var activeAlerts = await _stockAlertRepository.Query()
                .AsNoTracking()
                .Where(a => (!clientId.HasValue || a.ClientId == clientId.Value) && a.Status == StockAlertStatus.Active)
                .CountAsync(cancellationToken);
            response.ActiveStockAlerts = activeAlerts;

            // Müşteri İstatistikleri (optimize: AsNoTracking kullan)
            var customers = _customerRepository.Query()
                .AsNoTracking()
                .Where(c => !clientId.HasValue || c.ClientId == clientId.Value);
            response.TotalCustomers = await customers.CountAsync(cancellationToken);

            var vehicles = _vehicleRepository.Query()
                .AsNoTracking()
                .Where(v => !clientId.HasValue || v.ClientId == clientId.Value);
            response.TotalVehicles = await vehicles.CountAsync(cancellationToken);

            var newCustomersThisMonth = await customers
                .Where(c => c.CreatedDate >= DateTime.UtcNow.AddMonths(-1))
                .CountAsync(cancellationToken);
            response.NewCustomersThisMonth = newCustomersThisMonth;

            // Teklif İstatistikleri (optimize: AsNoTracking kullan)
            var openQuotes = await _quoteRequestRepository.Query()
                .AsNoTracking()
                .Where(qr => (!clientId.HasValue || qr.ClientId == clientId.Value) && qr.Status == QuoteStatus.Open)
                .CountAsync(cancellationToken);
            response.OpenQuoteRequests = openQuotes;

            var pendingQuotes = await _quoteResponseRepository.Query()
                .AsNoTracking()
                .Where(qr => (!clientId.HasValue || qr.ClientId == clientId.Value) && qr.Status == "Pending")
                .CountAsync(cancellationToken);
            response.PendingQuoteResponses = pendingQuotes;

            // Bildirim İstatistikleri
            var unreadNotifications = await _notificationRepository.Query()
                .AsNoTracking()
                .Where(n => (!clientId.HasValue || n.ClientId == clientId.Value) &&
                           n.Status != NotificationStatus.Read)
                .CountAsync(cancellationToken);
            response.UnreadNotifications = unreadNotifications;

            return response;
        }
    }
}

