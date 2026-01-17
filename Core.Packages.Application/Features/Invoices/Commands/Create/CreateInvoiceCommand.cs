using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.Create
{
    public class CreateInvoiceCommand : IRequest<CreateInvoiceResponse>
    {
        public InvoiceType InvoiceType { get; set; }
        public int? WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }

    public class InvoiceItemDto
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal TaxRate { get; set; } = 20;
    }
}

