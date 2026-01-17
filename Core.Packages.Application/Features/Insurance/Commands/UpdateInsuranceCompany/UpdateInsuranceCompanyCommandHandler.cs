using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceCompany
{
    public class UpdateInsuranceCompanyCommandHandler : IRequestHandler<UpdateInsuranceCompanyCommand, UpdateInsuranceCompanyResponse>
    {
        private readonly IInsuranceCompanyRepository _insuranceCompanyRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateInsuranceCompanyCommandHandler(
            IInsuranceCompanyRepository insuranceCompanyRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _insuranceCompanyRepository = insuranceCompanyRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateInsuranceCompanyResponse> Handle(UpdateInsuranceCompanyCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var insuranceCompany = await _insuranceCompanyRepository.GetByIdAsync(request.Id);
            if (insuranceCompany == null || insuranceCompany.ClientId != clientId)
            {
                throw new DomainException("INSURANCE_COMPANY_NOT_FOUND", new { Id = request.Id });
            }

            // Update fields (CompanyCode is immutable)
            insuranceCompany.CompanyName = request.CompanyName;
            insuranceCompany.ContactPerson = request.ContactPerson;
            insuranceCompany.Phone = request.Phone;
            insuranceCompany.Email = request.Email;
            insuranceCompany.Address = request.Address;
            insuranceCompany.ApiEndpoint = request.ApiEndpoint;
            insuranceCompany.ApiKey = request.ApiKey; // Should be encrypted in production
            insuranceCompany.SupportedInsuranceTypes = request.SupportedInsuranceTypes;
            insuranceCompany.IsActive = request.IsActive;

            _insuranceCompanyRepository.Update(insuranceCompany);
            await _insuranceCompanyRepository.SaveChangesAsync();

            return _mapper.Map<UpdateInsuranceCompanyResponse>(insuranceCompany);
        }
    }
}
