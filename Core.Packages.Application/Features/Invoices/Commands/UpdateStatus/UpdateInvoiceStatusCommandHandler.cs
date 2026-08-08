using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.UpdateStatus
{
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, UpdateInvoiceStatusResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateInvoiceStatusCommandHandler(
            IInvoiceRepository invoiceRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateInvoiceStatusResponse> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Faturayı bul
            var invoice = await _invoiceRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
            {
                throw new DomainException("INVOICE_NOT_FOUND", new { InvoiceId = request.InvoiceId });
            }

            // ClientId kontrolü
            if (invoice.ClientId != clientId)
            {
                throw new DomainException("INVOICE_NOT_BELONG_TO_CLIENT", new { InvoiceId = request.InvoiceId });
            }

            var oldStatus = invoice.Status;

            // Ödeme tutarını güncelle
            if (request.PaidAmount.HasValue)
            {
                invoice.PaidAmount = request.PaidAmount.Value;
            }

            // Durumu güncelle
            invoice.Status = request.Status;
            invoice.UpdatePaymentStatus();

            // Notes güncelle
            if (!string.IsNullOrEmpty(request.Notes))
            {
                invoice.Description = request.Notes;
            }

            _invoiceRepository.Update(invoice);
            await _invoiceRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateInvoiceStatusResponse>(invoice);
            response.OldStatus = oldStatus;
            response.NewStatus = invoice.Status;
            response.StatusName = invoice.Status.ToString();

            return response;
        }
    }
}

