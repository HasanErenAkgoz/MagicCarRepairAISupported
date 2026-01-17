namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetRecentActivities
{
    public class GetRecentActivitiesResponse
    {
        public string ActivityType { get; set; } // "WorkOrder", "Invoice", "Customer", "Quote", etc.
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ActivityDate { get; set; }
        public int? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; }
    }
}

