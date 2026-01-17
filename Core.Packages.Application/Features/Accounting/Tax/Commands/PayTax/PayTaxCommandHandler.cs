using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.PayTax
{
    public class PayTaxCommandHandler : IRequestHandler<PayTaxCommand, PayTaxResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public PayTaxCommandHandler(
            ITaxRepository taxRepository,
            IMediator mediator,
            ITenantService tenantService,
            IMapper mapper)
        {
            _taxRepository = taxRepository;
            _mediator = mediator;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<PayTaxResponse> Handle(PayTaxCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Tax'ı bul
            var tax = await _taxRepository.GetByIdAsync(request.Id);
            if (tax == null)
            {
                throw new DomainException("TAX_NOT_FOUND", new { Id = request.Id });
            }

            if (tax.ClientId != clientId)
            {
                throw new DomainException("TAX_NOT_BELONG_TO_CLIENT", new { Id = request.Id });
            }

            // Zaten ödenmişse
            if (tax.Status == TaxStatus.Paid)
            {
                throw new DomainException("TAX_ALREADY_PAID", new { Id = request.Id });
            }

            // Ödeme yap
            tax.MarkAsPaid(request.PaymentDate, request.PaymentMethod, request.PaymentReferenceNumber);

            // Kaydet
            _taxRepository.Update(tax);
            await _taxRepository.SaveChangesAsync();

            // Business Rule: Vergi ödendiğinde otomatik gider kaydı oluştur
            var createExpenseCommand = new CreateExpenseCommand
            {
                ExpenseType = ExpenseType.Tax,
                Amount = tax.Amount,
                PaymentMethod = request.PaymentMethod,
                TransactionDate = request.PaymentDate,
                Description = $"{tax.TaxType} vergisi ödemesi - {tax.TaxOffice}",
                InvoiceNumber = request.PaymentReferenceNumber
            };

            await _mediator.Send(createExpenseCommand, cancellationToken);

            // Response
            var response = _mapper.Map<PayTaxResponse>(tax);
            response.TaxTypeName = tax.TaxType.ToString();
            response.StatusName = tax.Status.ToString();
            response.PaymentMethodName = tax.PaymentMethod?.ToString() ?? string.Empty;

            return response;
        }
    }
}
