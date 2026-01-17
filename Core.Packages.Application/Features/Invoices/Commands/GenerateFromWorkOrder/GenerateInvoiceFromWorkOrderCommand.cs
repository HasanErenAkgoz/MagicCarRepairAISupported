using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder
{
    public class GenerateInvoiceFromWorkOrderCommand : IRequest<GenerateInvoiceFromWorkOrderResponse>
    {
        public int WorkOrderId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
    }
}

