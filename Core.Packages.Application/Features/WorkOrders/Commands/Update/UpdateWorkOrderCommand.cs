using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Update
{
    public class UpdateWorkOrderCommand : IRequest<UpdateWorkOrderResponse>
    {
        public int WorkOrderId { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public WorkOrderPriority? Priority { get; set; }
        public long? Kilometers { get; set; }
        public int? FuelLevel { get; set; }
        public string? CustomerComplaints { get; set; }
        public string? SpecialRequests { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? Notes { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}
