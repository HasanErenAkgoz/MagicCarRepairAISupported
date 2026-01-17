namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicPortfolio
{
    public class GetPublicPortfolioResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Categories { get; set; }
        public List<string>? FeaturedPhotoUrls { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        
        // Vehicle info (anonymized)
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }
    }
}
