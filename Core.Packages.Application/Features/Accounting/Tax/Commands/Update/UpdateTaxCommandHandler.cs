using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Update
{
    public class UpdateTaxCommandHandler : IRequestHandler<UpdateTaxCommand, UpdateTaxResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateTaxCommandHandler(
            ITaxRepository taxRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _taxRepository = taxRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateTaxResponse> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
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

            // Ödenmiş vergi güncellenemez
            if (tax.Status == TaxStatus.Paid)
            {
                throw new DomainException("TAX_ALREADY_PAID", new { Id = request.Id });
            }

            // Güncelle
            tax.Amount = request.Amount;
            tax.DueDate = request.DueDate;
            tax.Description = request.Description;
            tax.TaxOffice = request.TaxOffice;
            tax.TaxNumber = request.TaxNumber;

            // Vadesi geçmişse durumu güncelle
            if (tax.IsOverdue())
            {
                tax.Status = TaxStatus.Overdue;
            }

            // Kaydet
            _taxRepository.Update(tax);
            await _taxRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateTaxResponse>(tax);
            response.TaxTypeName = tax.TaxType.ToString();
            response.StatusName = tax.Status.ToString();

            return response;
        }
    }
}
