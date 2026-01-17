using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.PartUsageReport
{
    public class GetPartUsageReportQueryHandler : IRequestHandler<GetPartUsageReportQuery, GetPartUsageReportResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IPartRepository _partRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly ITenantService _tenantService;

        public GetPartUsageReportQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IPartRepository partRepository,
            IStockMovementRepository stockMovementRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _partRepository = partRepository;
            _stockMovementRepository = stockMovementRepository;
            _tenantService = tenantService;
        }

        public async Task<GetPartUsageReportResponse> Handle(GetPartUsageReportQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");
            var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-1);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            // İş emri kalemlerinden parça kullanımını al
            var workOrders = await _workOrderRepository.Query()
                .Where(wo => wo.ClientId == clientId &&
                           wo.EntryDate >= startDate &&
                           wo.EntryDate <= endDate)
                .Include(wo => wo.Items)
                    .ThenInclude(item => item.Part)
                .ToListAsync(cancellationToken);

            // Parça kullanım istatistiklerini hesapla
            var partUsage = workOrders
                .SelectMany(wo => wo.Items)
                .Where(item => item.ItemType == WorkOrderItemType.Part && item.PartId.HasValue)
                .GroupBy(item => item.PartId!.Value)
                .Select(g => new
                {
                    PartId = g.Key,
                    Part = g.First().Part,
                    TotalQuantity = g.Sum(item => (double)item.Quantity),
                    TotalRevenue = g.Sum(item => item.TotalAmount),
                    UsageCount = g.Count(),
                    AveragePrice = g.Average(item => item.UnitPrice)
                })
                .ToList();

            // Part bilgilerini al
            var partIds = partUsage.Select(pu => pu.PartId).ToList();
            var parts = await _partRepository.Query()
                .Where(p => partIds.Contains(p.Id))
                .Include(p => p.Stock)
                .ToListAsync(cancellationToken);

            // En çok kullanılan parçalar
            var topUsedParts = partUsage
                .OrderByDescending(pu => pu.TotalQuantity)
                .Take(request.TopN)
                .Select(pu =>
                {
                    var part = parts.FirstOrDefault(p => p.Id == pu.PartId);
                    var stock = part?.Stock;
                    return new PartUsageItem
                    {
                        PartId = pu.PartId,
                        PartCode = part?.PartCode ?? "N/A",
                        PartName = part?.Name ?? pu.Part?.Name ?? "N/A",
                        Category = part?.Category ?? PartCategory.Other,
                        TotalQuantityUsed = (int)pu.TotalQuantity,
                        TotalRevenue = pu.TotalRevenue,
                        UsageCount = pu.UsageCount,
                        AveragePrice = pu.AveragePrice,
                        CurrentStock = stock?.Quantity ?? 0,
                        PurchasePrice = part?.PurchasePrice ?? 0,
                        ProfitMargin = part != null && pu.AveragePrice > 0
                            ? (double)((pu.AveragePrice - part.PurchasePrice) / pu.AveragePrice * 100)
                            : 0
                    };
                })
                .ToList();

            // Parça bazlı kar marjları
            var partProfitMargins = topUsedParts
                .Select(pu => new PartProfitMarginItem
                {
                    PartId = pu.PartId,
                    PartCode = pu.PartCode,
                    PartName = pu.PartName,
                    TotalRevenue = pu.TotalRevenue,
                    TotalCost = pu.TotalQuantityUsed * pu.PurchasePrice,
                    TotalProfit = pu.TotalRevenue - (pu.TotalQuantityUsed * pu.PurchasePrice),
                    ProfitMargin = pu.ProfitMargin
                })
                .OrderByDescending(p => p.TotalProfit)
                .ToList();

            // Tüketim trendleri (aylık)
            var monthlyConsumption = workOrders
                .SelectMany(wo => wo.Items)
                .Where(item => item.ItemType == WorkOrderItemType.Part && item.PartId.HasValue &&
                              (request.PartId == null || item.PartId == request.PartId))
                .GroupBy(item => new { item.WorkOrder.EntryDate.Year, item.WorkOrder.EntryDate.Month })
                .Select(g => new ConsumptionTrendItem
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    TotalQuantity = (int)g.Sum(item => (double)item.Quantity),
                    TotalRevenue = g.Sum(item => item.TotalAmount),
                    PartCount = g.Select(item => item.PartId!.Value).Distinct().Count()
                })
                .OrderBy(t => t.Period)
                .ToList();

            // Stok dönüş hızı (Turnover Rate)
            var stockTurnoverRates = new List<StockTurnoverItem>();
            foreach (var partUsageItem in topUsedParts)
            {
                var part = parts.FirstOrDefault(p => p.Id == partUsageItem.PartId);
                if (part != null && part.Stock != null)
                {
                    var averageStock = part.Stock.Quantity; // Basitleştirilmiş - gerçekte ortalama stok hesaplanmalı
                    var turnoverRate = averageStock > 0
                        ? (partUsageItem.TotalQuantityUsed / (double)averageStock)
                        : 0;

                    stockTurnoverRates.Add(new StockTurnoverItem
                    {
                        PartId = partUsageItem.PartId,
                        PartCode = partUsageItem.PartCode,
                        PartName = partUsageItem.PartName,
                        AverageStock = averageStock,
                        TotalUsage = partUsageItem.TotalQuantityUsed,
                        TurnoverRate = Math.Round(turnoverRate, 2)
                    });
                }
            }

            return new GetPartUsageReportResponse
            {
                StartDate = startDate,
                EndDate = endDate,
                TopUsedParts = topUsedParts,
                PartProfitMargins = partProfitMargins,
                MonthlyConsumption = monthlyConsumption,
                StockTurnoverRates = stockTurnoverRates.OrderByDescending(st => st.TurnoverRate).ToList()
            };
        }
    }
}

