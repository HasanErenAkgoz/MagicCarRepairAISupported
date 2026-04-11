using System.Text.Json.Serialization;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWeeklyRevenue
{
    public class GetWeeklyRevenueResponse
    {
        public decimal WeeklyTotal { get; set; }
        public decimal PreviousWeekTotal { get; set; }
        public decimal ChangePercent { get; set; }
        public List<DailyRevenueData> DailyData { get; set; } = new();
    }

    public class DailyRevenueData
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty; // ISO 8601 format: "YYYY-MM-DD"
        
        [JsonPropertyName("dayOfWeek")]
        public string DayOfWeek { get; set; } = string.Empty; // "Monday", "Tuesday", etc.
        
        [JsonPropertyName("dayShort")]
        public string DayShort { get; set; } = string.Empty; // "Mon", "Tue", etc.
        
        [JsonPropertyName("revenue")]
        public decimal Revenue { get; set; }
    }
}
