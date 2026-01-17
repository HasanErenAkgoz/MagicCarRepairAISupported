using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.Create
{
    public class CreateInvoiceResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string InvoiceTypeName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int ItemCount { get; set; }
    }
}

