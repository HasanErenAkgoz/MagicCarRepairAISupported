using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetRecentActivities
{
    public class GetRecentActivitiesQueryHandler : IRequestHandler<GetRecentActivitiesQuery, List<GetRecentActivitiesResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly ITenantService _tenantService;

        public GetRecentActivitiesQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IInvoiceRepository invoiceRepository,
            IEntityRepository<Customer, int> customerRepository,
            IQuoteRequestRepository quoteRequestRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _quoteRequestRepository = quoteRequestRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetRecentActivitiesResponse>> Handle(GetRecentActivitiesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();
            var activities = new List<GetRecentActivitiesResponse>();

            // Son İş Emirleri
            var recentWorkOrders = await _workOrderRepository.Query()
                .Where(wo => wo.ClientId == clientId)
                .OrderByDescending(wo => wo.CreatedDate)
                .Take(request.Count)
                .ToListAsync(cancellationToken);

            activities.AddRange(recentWorkOrders.Select(wo => new GetRecentActivitiesResponse
            {
                ActivityType = "WorkOrder",
                Title = $"Yeni İş Emri: {wo.WorkOrderNumber}",
                Description = $"Durum: {wo.Status}",
                ActivityDate = wo.CreatedDate ?? DateTime.UtcNow,
                RelatedEntityId = wo.Id,
                RelatedEntityType = "WorkOrder"
            }));

            // Son Faturalar
            var recentInvoices = await _invoiceRepository.Query()
                .Where(i => i.ClientId == clientId)
                .OrderByDescending(i => i.CreatedDate)
                .Take(request.Count)
                .ToListAsync(cancellationToken);

            activities.AddRange(recentInvoices.Select(inv => new GetRecentActivitiesResponse
            {
                ActivityType = "Invoice",
                Title = $"Yeni Fatura: {inv.InvoiceNumber}",
                Description = $"Tutar: {inv.TotalAmount:C} - Durum: {inv.Status}",
                ActivityDate = inv.CreatedDate ?? DateTime.UtcNow,
                RelatedEntityId = inv.Id,
                RelatedEntityType = "Invoice"
            }));

            // Son Müşteriler
            var recentCustomers = await _customerRepository.Query()
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.CreatedDate)
                .Take(request.Count)
                .ToListAsync(cancellationToken);

            activities.AddRange(recentCustomers.Select(c => new GetRecentActivitiesResponse
            {
                ActivityType = "Customer",
                Title = $"Yeni Müşteri: {c.FullName}",
                Description = $"Email: {c.Email ?? "N/A"}",
                ActivityDate = c.CreatedDate ?? DateTime.UtcNow,
                RelatedEntityId = c.Id,
                RelatedEntityType = "Customer"
            }));

            // Son Teklif Talepleri
            var recentQuotes = await _quoteRequestRepository.Query()
                .Where(qr => qr.ClientId == clientId)
                .OrderByDescending(qr => qr.CreatedDate)
                .Take(request.Count)
                .ToListAsync(cancellationToken);

            activities.AddRange(recentQuotes.Select(qr => new GetRecentActivitiesResponse
            {
                ActivityType = "Quote",
                Title = $"Yeni Teklif Talebi: QR-{qr.Id}",
                Description = $"Durum: {qr.Status}",
                ActivityDate = qr.CreatedDate ?? DateTime.UtcNow,
                RelatedEntityId = qr.Id,
                RelatedEntityType = "QuoteRequest"
            }));

            // Tarihe göre sırala ve istenen sayıda döndür
            return activities
                .OrderByDescending(a => a.ActivityDate)
                .Take(request.Count)
                .ToList();
        }
    }
}

