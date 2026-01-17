using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceCompany
{
    public class CreateInsuranceCompanyCommandHandler : IRequestHandler<CreateInsuranceCompanyCommand, CreateInsuranceCompanyResponse>
    {
        private readonly IInsuranceCompanyRepository _insuranceCompanyRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateInsuranceCompanyCommandHandler(
            IInsuranceCompanyRepository insuranceCompanyRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _insuranceCompanyRepository = insuranceCompanyRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateInsuranceCompanyResponse> Handle(CreateInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Check if company code already exists
            var existingCompany = await _insuranceCompanyRepository.GetByCompanyCodeAsync(request.CompanyCode, cancellationToken);
            if (existingCompany != null)
            {
                throw new DomainException("INSURANCE_COMPANY_CODE_ALREADY_EXISTS", new { CompanyCode = request.CompanyCode });
            }

            var insuranceCompany = new InsuranceCompany
            {
                CompanyName = request.CompanyName,
                CompanyCode = request.CompanyCode,
                ContactPerson = request.ContactPerson,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                ApiEndpoint = request.ApiEndpoint,
                ApiKey = request.ApiKey, // Should be encrypted in production
                SupportedInsuranceTypes = request.SupportedInsuranceTypes,
                IsActive = request.IsActive,
                ClientId = clientId
            };

            await _insuranceCompanyRepository.AddAsync(insuranceCompany, cancellationToken);

            return _mapper.Map<CreateInsuranceCompanyResponse>(insuranceCompany);
        }
    }
}

