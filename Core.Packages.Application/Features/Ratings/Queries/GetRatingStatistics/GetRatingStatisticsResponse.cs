namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingStatistics
{
    public class GetRatingStatisticsResponse
    {
        public int TotalRatings { get; set; }
        public decimal AverageRating { get; set; }
        public decimal AverageServiceQuality { get; set; }
        public decimal AveragePriceValue { get; set; }
        public decimal AverageOnTimeDelivery { get; set; }
        public decimal AverageStaffBehavior { get; set; }
        public RatingDistribution Distribution { get; set; } = new();
        public List<MonthlyRatingTrend> MonthlyTrends { get; set; } = new();
        public List<CategoryAverage> CategoryAverages { get; set; } = new();
    }

    public class RatingDistribution
    {
        public int FiveStar { get; set; }
        public int FourStar { get; set; }
        public int ThreeStar { get; set; }
        public int TwoStar { get; set; }
        public int OneStar { get; set; }
    }

    public class MonthlyRatingTrend
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal AverageRating { get; set; }
        public int RatingCount { get; set; }
    }

    public class CategoryAverage
    {
        public string Category { get; set; }
        public decimal Average { get; set; }
        public int Count { get; set; }
    }
}
