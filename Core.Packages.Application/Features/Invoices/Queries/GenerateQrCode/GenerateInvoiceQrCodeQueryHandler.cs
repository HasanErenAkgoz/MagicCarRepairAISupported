using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.GenerateQrCode
{
    public class GenerateInvoiceQrCodeQueryHandler : IRequestHandler<GenerateInvoiceQrCodeQuery, byte[]>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IQrCodeService _qrCodeService;

        public GenerateInvoiceQrCodeQueryHandler(
            IInvoiceRepository invoiceRepository,
            IQrCodeService qrCodeService)
        {
            _invoiceRepository = invoiceRepository;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> Handle(GenerateInvoiceQrCodeQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.Query()
                .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

            if (invoice == null)
            {
                throw new InvalidOperationException("Invoice not found");
            }

            var qrCodeData = _qrCodeService.GenerateInvoiceQrCodeData(invoice.Id, invoice.TotalAmount);
            return _qrCodeService.GenerateQrCode(qrCodeData, request.Size);
        }
    }
}






