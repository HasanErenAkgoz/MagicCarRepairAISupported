using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById
{
    public class GetInvoiceByIdResponse
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public string InvoiceTypeName { get; set; }
        public int? WorkOrderId { get; set; }
        public string? WorkOrderNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public string StatusName { get; set; }
        public string? Description { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
        public DateTime CreatedDate { get; set; }
    }

    public class InvoiceItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

