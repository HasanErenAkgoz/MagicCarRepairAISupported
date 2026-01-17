using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Deliver
{
    public class DeliverWorkOrderCommand : IRequest<DeliverWorkOrderResponse>
    {
        public int WorkOrderId { get; set; }
        public decimal? PaidAmount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public int? EmployeeId { get; set; }
        /// <summary>
        /// Müşteri onayı gerekip gerekmediği (varsayılan: false - direkt teslim)
        /// </summary>
        public bool RequireCustomerApproval { get; set; } = false;
    }
}

