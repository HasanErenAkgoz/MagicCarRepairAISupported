using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.ForecastStock
{
    public class ForecastStockQuery : IRequest<ForecastStockResponse>
    {
        public int? PartId { get; set; }
        public int ForecastPeriodDays { get; set; } = 30;
        public int HistoricalDataDays { get; set; } = 90;
        public bool IncludeMinimumStockRecommendation { get; set; } = true;
        public bool IncludeOrderRecommendation { get; set; } = true;
    }
}
