namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddTimelineNote
{
    /// <summary>
    /// Mobil uygulama için iş emri timeline manuel not ekleme response'u
    /// Doküman: WORK_ORDER_PARTS_LABOR_TIMELINE.md
    /// </summary>
    public class AddWorkOrderTimelineNoteResponse
    {
        public TimelineNoteDto Data { get; set; } = new();
    }

    public class TimelineNoteDto
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = "note";
        public string Note { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty; // ISO 8601
        public string CreatedBy { get; set; } = string.Empty;
    }
}
