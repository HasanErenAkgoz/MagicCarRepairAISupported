using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using TaxEntity = MagicCarRepairAISupported.Domain.Entities.Tax;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Create
{
    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, CreateTaxResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateTaxCommandHandler(
            ITaxRepository taxRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _taxRepository = taxRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateTaxResponse> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // Validasyon
            if (request.Year < 2000 || request.Year > 2100)
            {
                throw new DomainException("INVALID_YEAR", new { Year = request.Year });
            }

            if (request.Month.HasValue && (request.Month < 1 || request.Month > 12))
            {
                throw new DomainException("INVALID_MONTH", new { Month = request.Month });
            }

            if (request.Amount <= 0)
            {
                throw new DomainException("INVALID_AMOUNT", new { Amount = request.Amount });
            }

            // Entity oluştur
            var tax = new TaxEntity
            {
                TaxType = request.TaxType,
                Month = request.Month,
                Year = request.Year,
                Amount = request.Amount,
                DueDate = request.DueDate,
                Status = TaxStatus.Pending,
                Description = request.Description,
                TaxOffice = request.TaxOffice,
                TaxNumber = request.TaxNumber,
                ClientId = clientId
            };

            // Kaydet
            await _taxRepository.AddAsync(tax, cancellationToken);
            await _taxRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateTaxResponse>(tax);
            response.TaxTypeName = tax.TaxType.ToString();
            response.StatusName = tax.Status.ToString();

            return response;
        }
    }
}
