namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.BulkDeleteParts
{
    public class BulkDeletePartsResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public int RequestedCount { get; set; }
        public int DeletedCount { get; set; }
        public int SkippedCount { get; set; }

        public List<int> DeletedIds { get; set; } = new();
        public List<BulkDeletePartSkippedItem> Skipped { get; set; } = new();
    }

    public class BulkDeletePartSkippedItem
    {
        public int Id { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}

