using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Complete
{
    public class CompleteWorkOrderCommand : IRequest<CompleteWorkOrderResponse>
    {
        public int WorkOrderId { get; set; }
        public string? Notes { get; set; }
        public int? EmployeeId { get; set; }
    }
}

