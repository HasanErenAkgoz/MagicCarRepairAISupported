using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.PartUsageReport
{
    public class GetPartUsageReportResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PartUsageItem> TopUsedParts { get; set; } = new();
        public List<PartProfitMarginItem> PartProfitMargins { get; set; } = new();
        public List<ConsumptionTrendItem> MonthlyConsumption { get; set; } = new();
        public List<StockTurnoverItem> StockTurnoverRates { get; set; } = new();
    }

    public class PartUsageItem
    {
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public PartCategory Category { get; set; }
        public int TotalQuantityUsed { get; set; }
        public decimal TotalRevenue { get; set; }
        public int UsageCount { get; set; }
        public decimal AveragePrice { get; set; }
        public int CurrentStock { get; set; }
        public decimal PurchasePrice { get; set; }
        public double ProfitMargin { get; set; }
    }

    public class PartProfitMarginItem
    {
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalProfit { get; set; }
        public double ProfitMargin { get; set; }
    }

    public class ConsumptionTrendItem
    {
        public string Period { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PartCount { get; set; }
    }

    public class StockTurnoverItem
    {
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public int AverageStock { get; set; }
        public int TotalUsage { get; set; }
        public double TurnoverRate { get; set; }
    }
}

