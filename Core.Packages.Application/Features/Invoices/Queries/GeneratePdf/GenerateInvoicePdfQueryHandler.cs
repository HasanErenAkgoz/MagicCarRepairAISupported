using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GeneratePdf
{
    public class GenerateInvoicePdfQueryHandler : IRequestHandler<GenerateInvoicePdfQuery, byte[]>
    {
        private readonly IPdfInvoiceService _pdfInvoiceService;

        public GenerateInvoicePdfQueryHandler(IPdfInvoiceService pdfInvoiceService)
        {
            _pdfInvoiceService = pdfInvoiceService;
        }

        public async Task<byte[]> Handle(GenerateInvoicePdfQuery request, CancellationToken cancellationToken)
        {
            return await _pdfInvoiceService.GenerateInvoicePdfAsync(request.InvoiceId, cancellationToken);
        }
    }
}






