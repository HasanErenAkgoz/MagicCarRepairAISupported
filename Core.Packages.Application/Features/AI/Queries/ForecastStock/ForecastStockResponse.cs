namespace MagicCarRepairAISupported.Application.Features.AI.Queries.ForecastStock
{
    public class ForecastStockResponse
    {
        public List<PartStockForecastDto> Forecasts { get; set; } = new();
        public string Summary { get; set; } = string.Empty;
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    public class PartStockForecastDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string PartCode { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int MinimumStockLevel { get; set; }
        public int PredictedConsumption { get; set; }
        public int PredictedEndStock { get; set; }
        public string StockoutRisk { get; set; } = string.Empty;
        public DateTime? PredictedStockoutDate { get; set; }
        public int? RecommendedMinimumStock { get; set; }
        public int? RecommendedOrderQuantity { get; set; }
        public string Trend { get; set; } = string.Empty;
        public int ConfidenceScore { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public double AverageDailyConsumption { get; set; }
    }
}
