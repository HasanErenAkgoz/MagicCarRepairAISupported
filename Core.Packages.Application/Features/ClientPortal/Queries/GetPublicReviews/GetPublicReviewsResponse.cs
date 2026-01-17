namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicReviews
{
    public class GetPublicReviewsResponse
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CustomerName { get; set; } // Anonymized: "Müşteri A", "Müşteri B", etc.
        public string? WorkOrderNumber { get; set; }
    }
}
