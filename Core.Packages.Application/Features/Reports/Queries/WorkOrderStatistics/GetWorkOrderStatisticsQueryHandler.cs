using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.WorkOrderStatistics
{
    public class GetWorkOrderStatisticsQueryHandler : IRequestHandler<GetWorkOrderStatisticsQuery, GetWorkOrderStatisticsResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetWorkOrderStatisticsQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<GetWorkOrderStatisticsResponse> Handle(GetWorkOrderStatisticsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");
            var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-1);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            // Query oluştur
            var workOrdersQuery = _workOrderRepository.Query()
                .Where(wo => wo.ClientId == clientId &&
                           wo.EntryDate >= startDate &&
                           wo.EntryDate <= endDate);

            // Filtreleme
            if (request.IncludeCompletedOnly)
            {
                workOrdersQuery = workOrdersQuery.Where(wo => wo.Status == WorkOrderStatus.Delivered);
            }

            if (request.EmployeeId.HasValue)
            {
                workOrdersQuery = workOrdersQuery.Where(wo => wo.AssignedEmployeeId == request.EmployeeId.Value);
            }

            var workOrders = await workOrdersQuery
                .Include(wo => wo.AssignedEmployee)
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .ToListAsync(cancellationToken);

            // Ortalama tamamlanma süresi hesapla (tamamlanmış iş emirleri için)
            var completedWorkOrders = workOrders
                .Where(wo => wo.Status == WorkOrderStatus.Delivered && wo.ActualDeliveryDate.HasValue)
                .ToList();

            var avgCompletionDays = completedWorkOrders.Any()
                ? completedWorkOrders
                    .Select(wo => (wo.ActualDeliveryDate!.Value - wo.EntryDate).TotalDays)
                    .Average()
                : 0;

            // Personel performans analizi
            var employeePerformance = workOrders
                .Where(wo => wo.AssignedEmployeeId.HasValue && wo.Status == WorkOrderStatus.Delivered && wo.ActualDeliveryDate.HasValue)
                .GroupBy(wo => wo.AssignedEmployeeId)
                .Select(g => new EmployeePerformanceItem
                {
                    EmployeeId = g.Key!.Value,
                    EmployeeName = g.First().AssignedEmployee != null
                        ? $"{g.First().AssignedEmployee!.FirstName} {g.First().AssignedEmployee!.LastName}"
                        : "Unknown",
                    CompletedCount = g.Count(),
                    AverageCompletionDays = g.Average(wo => (wo.ActualDeliveryDate!.Value - wo.EntryDate).TotalDays),
                    TotalRevenue = g.Sum(wo => wo.TotalAmount)
                })
                .OrderByDescending(e => e.CompletedCount)
                .ToList();

            // Durum dağılımı
            var statusDistribution = workOrders
                .GroupBy(wo => wo.Status)
                .Select(g => new StatusDistributionItem
                {
                    Status = g.Key,
                    StatusName = g.Key.ToString(),
                    Count = g.Count(),
                    Percentage = (double)g.Count() / workOrders.Count * 100
                })
                .ToList();

            // Aylık/haftalık trend analizi
            var monthlyTrend = workOrders
                .GroupBy(wo => new { wo.EntryDate.Year, wo.EntryDate.Month })
                .Select(g => new TrendItem
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Count = g.Count(),
                    TotalRevenue = g.Sum(wo => wo.TotalAmount),
                    CompletedCount = g.Count(wo => wo.Status == WorkOrderStatus.Delivered)
                })
                .OrderBy(t => t.Period)
                .ToList();

            var weeklyTrend = workOrders
                .Where(wo => wo.EntryDate >= DateTime.UtcNow.AddDays(-90)) // Son 90 gün
                .GroupBy(wo => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    wo.EntryDate, 
                    System.Globalization.CalendarWeekRule.FirstDay, 
                    DayOfWeek.Monday))
                .Select(g => new TrendItem
                {
                    Period = $"Week {g.Key}",
                    Count = g.Count(),
                    TotalRevenue = g.Sum(wo => wo.TotalAmount),
                    CompletedCount = g.Count(wo => wo.Status == WorkOrderStatus.Delivered)
                })
                .OrderByDescending(t => t.Period)
                .Take(12) // Son 12 hafta
                .ToList();

            return new GetWorkOrderStatisticsResponse
            {
                TotalWorkOrders = workOrders.Count,
                CompletedWorkOrders = completedWorkOrders.Count,
                AverageCompletionDays = Math.Round(avgCompletionDays, 2),
                EmployeePerformance = employeePerformance,
                StatusDistribution = statusDistribution,
                MonthlyTrend = monthlyTrend,
                WeeklyTrend = weeklyTrend,
                TotalRevenue = workOrders.Sum(wo => wo.TotalAmount),
                AverageRevenue = workOrders.Any() ? workOrders.Average(wo => wo.TotalAmount) : 0
            };
        }
    }
}

