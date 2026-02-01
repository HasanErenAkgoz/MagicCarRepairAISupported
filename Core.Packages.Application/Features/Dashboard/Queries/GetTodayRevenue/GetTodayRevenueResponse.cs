namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTodayRevenue
{
    public class GetTodayRevenueResponse
    {
        public decimal TodayRevenue { get; set; }
        public decimal YesterdayRevenue { get; set; }
        public decimal ChangePercent { get; set; }
    }
}
