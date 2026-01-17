using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GeneratePdf
{
    public class GenerateInvoicePdfQuery : IRequest<byte[]>
    {
        public int InvoiceId { get; set; }
    }
}






