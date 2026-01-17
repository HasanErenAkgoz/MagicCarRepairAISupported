using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddLabor
{
    public class AddWorkOrderLaborCommand : IRequest<AddWorkOrderLaborResponse>
    {
        public int WorkOrderId { get; set; }
        public int EmployeeId { get; set; }
        public string OperationName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? DurationHours { get; set; }
        public decimal HourlyRate { get; set; }
        public string? Description { get; set; }
    }
}

