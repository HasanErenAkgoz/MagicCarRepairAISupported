using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GenerateQrCode
{
    public class GenerateInvoiceQrCodeQuery : IRequest<byte[]>
    {
        public int InvoiceId { get; set; }
        public int Size { get; set; } = 300;
    }
}






