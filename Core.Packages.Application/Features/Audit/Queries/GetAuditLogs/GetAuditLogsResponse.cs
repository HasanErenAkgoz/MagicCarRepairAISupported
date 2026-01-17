namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetAuditLogs
{
    public class GetAuditLogsResponse
    {
        public List<AuditLogItem> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public class AuditLogItem
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string Action { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? ChangedProperties { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? Description { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public long? DurationMs { get; set; }
        public string? RequestPath { get; set; }
        public string? RequestMethod { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
