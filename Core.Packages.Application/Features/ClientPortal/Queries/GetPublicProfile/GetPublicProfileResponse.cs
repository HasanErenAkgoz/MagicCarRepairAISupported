namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicProfile
{
    public class GetPublicProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? AboutUs { get; set; }
        public string? WorkingHours { get; set; }
        public string? Services { get; set; }
        public string? SocialMediaLinks { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        
        // Statistics
        public int TotalPortfolioItems { get; set; }
        public int TotalCertificates { get; set; }
        public int TotalTeamMembers { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }
}
