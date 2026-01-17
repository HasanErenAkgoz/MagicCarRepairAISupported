using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder
{
    public class GenerateInvoiceFromWorkOrderResponse
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}

