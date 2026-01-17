using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.CustomerAnalytics
{
    public class GetCustomerAnalyticsQueryHandler : IRequestHandler<GetCustomerAnalyticsQuery, GetCustomerAnalyticsResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITenantService _tenantService;

        public GetCustomerAnalyticsQueryHandler(
            ICustomerRepository customerRepository,
            IWorkOrderRepository workOrderRepository,
            IInvoiceRepository invoiceRepository,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _invoiceRepository = invoiceRepository;
            _tenantService = tenantService;
        }

        public async Task<GetCustomerAnalyticsResponse> Handle(GetCustomerAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");
            var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-12);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            // Müşterileri al
            var customersQuery = _customerRepository.Query()
                .Where(c => c.ClientId == clientId);

            if (request.CustomerId.HasValue)
            {
                customersQuery = customersQuery.Where(c => c.Id == request.CustomerId.Value);
            }

            var customers = await customersQuery.ToListAsync(cancellationToken);
            var customerIds = customers.Select(c => c.Id).ToList();

            // İş emirlerini al
            var workOrders = await _workOrderRepository.Query()
                .Where(wo => wo.ClientId == clientId &&
                           customerIds.Contains(wo.CustomerId) &&
                           wo.EntryDate >= startDate &&
                           wo.EntryDate <= endDate)
                .ToListAsync(cancellationToken);

            // Faturaları al
            var invoices = await _invoiceRepository.Query()
                .Where(inv => inv.ClientId == clientId &&
                            inv.CustomerId.HasValue &&
                            customerIds.Contains(inv.CustomerId.Value) &&
                            inv.InvoiceDate >= startDate &&
                            inv.InvoiceDate <= endDate)
                .ToListAsync(cancellationToken);

            // Müşteri sadakat skorları ve analitikleri hesapla
            var customerAnalytics = customers.Select(customer =>
            {
                var customerId = customer.Id;
                var customerWorkOrders = workOrders.Where(wo => wo.CustomerId == customerId).ToList();
                var customerInvoices = invoices.Where(inv => inv.CustomerId.HasValue && inv.CustomerId.Value == customerId).ToList();

                var totalSpent = customerInvoices.Sum(inv => inv.TotalAmount);
                var visitCount = customerWorkOrders.Count;
                var completedOrders = customerWorkOrders.Count(wo => wo.Status == WorkOrderStatus.Delivered);
                var totalRevenue = customerWorkOrders.Sum(wo => wo.TotalAmount);

                // Son ziyaret
                var lastVisit = customerWorkOrders.OrderByDescending(wo => wo.EntryDate).FirstOrDefault()?.EntryDate;

                // Ortalama ziyaret tutarı
                var averageVisitAmount = visitCount > 0 ? totalRevenue / visitCount : 0;

                // Tekrar gelme oranı (2+ ziyaret varsa)
                var returnRate = visitCount >= 2 ? 100 : (visitCount == 1 ? 50 : 0);

                // Customer Lifetime Value (CLV) - basitleştirilmiş hesaplama
                var clv = totalSpent; // Gerçekte gelecekteki değeri de tahmin edilmeli

                // Loyalty Score (0-100)
                var loyaltyScore = CalculateLoyaltyScore(visitCount, completedOrders, totalSpent, lastVisit);

                // Segmentasyon
                var segment = DetermineCustomerSegment(visitCount, totalSpent, lastVisit);

                return new CustomerAnalyticsItem
                {
                    CustomerId = customer.Id,
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    TotalVisits = visitCount,
                    CompletedOrders = completedOrders,
                    TotalSpent = totalSpent,
                    TotalRevenue = totalRevenue,
                    AverageVisitAmount = averageVisitAmount,
                    LastVisitDate = lastVisit,
                    ReturnRate = returnRate,
                    CustomerLifetimeValue = clv,
                    LoyaltyScore = loyaltyScore,
                    Segment = segment
                };
            })
            .OrderByDescending(ca => ca.TotalSpent)
            .ToList();

            // Segmentasyon dağılımı
            var segmentDistribution = customerAnalytics
                .GroupBy(ca => ca.Segment)
                .Select(g => new SegmentDistributionItem
                {
                    Segment = g.Key,
                    Count = g.Count(),
                    Percentage = (double)g.Count() / customerAnalytics.Count * 100,
                    TotalRevenue = g.Sum(ca => ca.TotalSpent)
                })
                .ToList();

            // Tekrar gelme oranı genel istatistiği
            var repeatCustomers = customerAnalytics.Count(ca => ca.TotalVisits >= 2);
            var repeatRate = customerAnalytics.Count > 0
                ? (double)repeatCustomers / customerAnalytics.Count * 100
                : 0;

            // Top müşteriler
            var topCustomers = customerAnalytics
                .OrderByDescending(ca => ca.TotalSpent)
                .Take(10)
                .ToList();

            return new GetCustomerAnalyticsResponse
            {
                StartDate = startDate,
                EndDate = endDate,
                CustomerAnalytics = customerAnalytics,
                SegmentDistribution = segmentDistribution,
                RepeatRate = Math.Round(repeatRate, 2),
                TopCustomers = topCustomers,
                AverageLoyaltyScore = customerAnalytics.Any() ? customerAnalytics.Average(ca => ca.LoyaltyScore) : 0,
                AverageCLV = customerAnalytics.Any() ? customerAnalytics.Average(ca => ca.CustomerLifetimeValue) : 0
            };
        }

        private int CalculateLoyaltyScore(int visitCount, int completedOrders, decimal totalSpent, DateTime? lastVisit)
        {
            var score = 0;

            // Ziyaret sayısı (40 puan)
            if (visitCount >= 10) score += 40;
            else if (visitCount >= 5) score += 30;
            else if (visitCount >= 3) score += 20;
            else if (visitCount >= 2) score += 10;
            else if (visitCount == 1) score += 5;

            // Tamamlanma oranı (20 puan)
            var completionRate = visitCount > 0 ? (double)completedOrders / visitCount : 0;
            score += (int)(completionRate * 20);

            // Harcama miktarı (30 puan)
            if (totalSpent >= 10000) score += 30;
            else if (totalSpent >= 5000) score += 20;
            else if (totalSpent >= 2000) score += 15;
            else if (totalSpent >= 1000) score += 10;
            else if (totalSpent > 0) score += 5;

            // Son ziyaret yakınlığı (10 puan)
            if (lastVisit.HasValue)
            {
                var daysSinceLastVisit = (DateTime.UtcNow - lastVisit.Value).TotalDays;
                if (daysSinceLastVisit <= 30) score += 10;
                else if (daysSinceLastVisit <= 90) score += 7;
                else if (daysSinceLastVisit <= 180) score += 4;
                else if (daysSinceLastVisit <= 365) score += 2;
            }

            return Math.Min(score, 100);
        }

        private string DetermineCustomerSegment(int visitCount, decimal totalSpent, DateTime? lastVisit)
        {
            if (visitCount >= 5 && totalSpent >= 5000)
            {
                return "VIP";
            }
            else if (visitCount >= 3 && totalSpent >= 2000)
            {
                return "Loyal";
            }
            else if (visitCount >= 2)
            {
                return "Regular";
            }
            else if (totalSpent >= 1000)
            {
                return "High Value";
            }
            else if (lastVisit.HasValue && (DateTime.UtcNow - lastVisit.Value).TotalDays <= 90)
            {
                return "Active";
            }
            else
            {
                return "New/Inactive";
            }
        }
    }
}

