namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetDataChangeHistory
{
    public class GetDataChangeHistoryResponse
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public List<ChangeHistoryItem> Changes { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class ChangeHistoryItem
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? ChangedProperties { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? IpAddress { get; set; }
    }
}
