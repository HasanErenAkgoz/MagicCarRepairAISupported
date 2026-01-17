using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.ApproveByCustomer
{
    public class ApproveWorkOrderByCustomerCommand : IRequest<ApproveWorkOrderByCustomerResponse>
    {
        public int WorkOrderId { get; set; }
        public string? Notes { get; set; }
    }
}
