namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetUserActivity
{
    public class GetUserActivityResponse
    {
        public int? UserId { get; set; }
        public List<UserActivityItem> Activities { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public ActivitySummary Summary { get; set; } = new();
    }

    public class UserActivityItem
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string Action { get; set; }
        public string? Description { get; set; }
        public string? IpAddress { get; set; }
        public string? RequestPath { get; set; }
        public string? RequestMethod { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class ActivitySummary
    {
        public int TotalActions { get; set; }
        public int CreateCount { get; set; }
        public int UpdateCount { get; set; }
        public int DeleteCount { get; set; }
        public int ViewCount { get; set; }
        public int FailedActions { get; set; }
        public Dictionary<string, int> ActionsByEntity { get; set; } = new();
    }
}
