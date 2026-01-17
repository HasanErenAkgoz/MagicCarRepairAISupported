namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetAverageRating
{
    public class GetAverageRatingResponse
    {
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int Rating5 { get; set; }
        public int Rating4 { get; set; }
        public int Rating3 { get; set; }
        public int Rating2 { get; set; }
        public int Rating1 { get; set; }
    }
}
