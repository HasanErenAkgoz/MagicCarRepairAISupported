using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetById
{
    public class GetTaxByIdQueryHandler : IRequestHandler<GetTaxByIdQuery, GetTaxByIdResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetTaxByIdQueryHandler(
            ITaxRepository taxRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _taxRepository = taxRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetTaxByIdResponse> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var tax = await _taxRepository.GetByIdAsync(request.Id);
            if (tax == null)
            {
                throw new DomainException("TAX_NOT_FOUND", new { Id = request.Id });
            }

            if (tax.ClientId != clientId)
            {
                throw new DomainException("TAX_NOT_BELONG_TO_CLIENT", new { Id = request.Id });
            }

            var response = _mapper.Map<GetTaxByIdResponse>(tax);
            response.TaxTypeName = tax.TaxType.ToString();
            response.StatusName = tax.Status.ToString();
            response.PaymentMethodName = tax.PaymentMethod?.ToString();
            response.IsOverdue = tax.IsOverdue();

            return response;
        }
    }
}
