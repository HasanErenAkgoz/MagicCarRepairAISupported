using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.UpdateStatus
{
    public class UpdateInvoiceStatusCommand : IRequest<UpdateInvoiceStatusResponse>
    {
        public int InvoiceId { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal? PaidAmount { get; set; }
        public string? Notes { get; set; }
    }
}

