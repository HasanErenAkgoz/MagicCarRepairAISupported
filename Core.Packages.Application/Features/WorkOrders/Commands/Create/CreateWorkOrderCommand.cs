using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create
{
    public class CreateWorkOrderCommand : IRequest<CreateWorkOrderResponse>
    {
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Normal;
        public long? Kilometers { get; set; }
        public int? FuelLevel { get; set; }
        public string? CustomerComplaints { get; set; }
        public string? SpecialRequests { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? Notes { get; set; }
    }
}

