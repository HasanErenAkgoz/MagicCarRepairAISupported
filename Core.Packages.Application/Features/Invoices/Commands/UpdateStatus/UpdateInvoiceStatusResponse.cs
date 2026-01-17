using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.UpdateStatus
{
    public class UpdateInvoiceStatusResponse
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceStatus OldStatus { get; set; }
        public InvoiceStatus NewStatus { get; set; }
        public string StatusName { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

