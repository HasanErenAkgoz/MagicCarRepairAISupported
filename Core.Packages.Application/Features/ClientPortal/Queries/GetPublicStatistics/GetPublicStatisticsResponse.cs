namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicStatistics
{
    public class GetPublicStatisticsResponse
    {
        public int TotalCompletedWorkOrders { get; set; }
        public int TotalPortfolioItems { get; set; }
        public int TotalActiveCustomers { get; set; }
        public int TotalTeamMembers { get; set; }
        public int TotalCertificates { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int YearsOfExperience { get; set; }
        public DateTime? EstablishedDate { get; set; }
    }
}
