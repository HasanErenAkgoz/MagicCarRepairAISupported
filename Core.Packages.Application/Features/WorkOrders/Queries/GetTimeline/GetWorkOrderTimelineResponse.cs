using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetTimeline
{
    public class GetWorkOrderTimelineResponse
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public WorkOrderStatus? OldStatus { get; set; }
        public string? OldStatusName { get; set; }
        public WorkOrderStatus? NewStatus { get; set; }
        public string? NewStatusName { get; set; }
        public string StatusChangeText { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
        public List<int>? PhotoIds { get; set; }
    }
}

