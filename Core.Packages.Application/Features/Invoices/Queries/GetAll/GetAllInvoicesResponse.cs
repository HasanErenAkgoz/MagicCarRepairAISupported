using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetAll
{
    public class GetAllInvoicesResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string InvoiceTypeName { get; set; }
        public int? WorkOrderId { get; set; }
        public string? WorkOrderNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public string StatusName { get; set; }
    }
}

