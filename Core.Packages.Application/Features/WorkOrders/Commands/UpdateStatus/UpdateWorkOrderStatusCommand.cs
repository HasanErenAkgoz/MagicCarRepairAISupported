using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus
{
    public class UpdateWorkOrderStatusCommand : IRequest<UpdateWorkOrderStatusResponse>
    {
        public int WorkOrderId { get; set; }
        public WorkOrderStatus NewStatus { get; set; }
        public string? Description { get; set; }
        public int? EmployeeId { get; set; } // Durum değişikliğini yapan personel
    }
}

