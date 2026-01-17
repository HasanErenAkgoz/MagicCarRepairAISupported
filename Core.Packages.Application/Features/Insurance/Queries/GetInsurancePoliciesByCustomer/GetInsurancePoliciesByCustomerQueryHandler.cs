using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByCustomer
{
    public class GetInsurancePoliciesByCustomerQueryHandler : IRequestHandler<GetInsurancePoliciesByCustomerQuery, GetInsurancePoliciesByCustomerResponse>
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetInsurancePoliciesByCustomerQueryHandler(
            IInsurancePolicyRepository insurancePolicyRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetInsurancePoliciesByCustomerResponse> Handle(GetInsurancePoliciesByCustomerQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var policies = await _insurancePolicyRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
            
            // Filter by client
            policies = policies.Where(p => p.ClientId == clientId).ToList();

            // Filter by active only if requested
            if (request.ActiveOnly == true)
            {
                policies = policies.Where(p => p.Status == InsuranceStatus.Active).ToList();
            }

            // Order by date (most recent first)
            policies = policies.OrderByDescending(p => p.StartDate).ToList();

            var policyDtos = policies.Select(p => new InsurancePolicyDto
            {
                Id = p.Id,
                PolicyNumber = p.PolicyNumber,
                VehicleId = p.VehicleId,
                VehicleBrand = p.Vehicle?.Brand ?? string.Empty,
                VehicleModel = p.Vehicle?.Model ?? string.Empty,
                VehicleLicensePlate = p.Vehicle?.LicensePlate ?? string.Empty,
                InsuranceCompanyId = p.InsuranceCompanyId,
                InsuranceCompanyName = p.InsuranceCompany?.CompanyName ?? string.Empty,
                InsuranceType = p.InsuranceType,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                PremiumAmount = p.PremiumAmount,
                CoverageAmount = p.CoverageAmount,
                Status = p.Status,
                DaysUntilExpiration = p.DaysUntilExpiration(),
                IsValid = p.IsValid()
            }).ToList();

            return new GetInsurancePoliciesByCustomerResponse
            {
                Policies = policyDtos
            };
        }
    }
}
