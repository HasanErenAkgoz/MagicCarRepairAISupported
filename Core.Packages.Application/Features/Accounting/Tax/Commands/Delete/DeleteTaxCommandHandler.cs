using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Delete
{
    public class DeleteTaxCommandHandler : IRequestHandler<DeleteTaxCommand, DeleteTaxResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;

        public DeleteTaxCommandHandler(
            ITaxRepository taxRepository,
            ITenantService tenantService)
        {
            _taxRepository = taxRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteTaxResponse> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

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

            // Ödenmiş vergi silinemez
            if (tax.Status == TaxStatus.Paid)
            {
                throw new DomainException("CANNOT_DELETE_PAID_TAX", new { Id = request.Id });
            }

            // Soft delete
            _taxRepository.Delete(tax);
            await _taxRepository.SaveChangesAsync();

            return new DeleteTaxResponse
            {
                Success = true,
                Message = "Vergi kaydı başarıyla silindi."
            };
        }
    }
}
