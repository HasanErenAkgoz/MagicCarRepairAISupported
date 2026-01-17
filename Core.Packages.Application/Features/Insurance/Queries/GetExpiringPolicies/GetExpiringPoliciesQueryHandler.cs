using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetExpiringPolicies
{
    public class GetExpiringPoliciesQueryHandler : IRequestHandler<GetExpiringPoliciesQuery, GetExpiringPoliciesResponse>
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly ITenantService _tenantService;

        public GetExpiringPoliciesQueryHandler(
            IInsurancePolicyRepository insurancePolicyRepository,
            ITenantService tenantService)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
            _tenantService = tenantService;
        }

        public async Task<GetExpiringPoliciesResponse> Handle(GetExpiringPoliciesQuery request, CancellationToken cancellationToken)
        {
            var policies = await _insurancePolicyRepository.GetExpiringPoliciesAsync(request.DaysBeforeExpiration, cancellationToken);

            var policiesDto = policies.Select(p => new ExpiringPolicyDto
            {
                Id = p.Id,
                PolicyNumber = p.PolicyNumber,
                VehicleLicensePlate = p.Vehicle?.LicensePlate ?? "N/A",
                VehicleBrandModel = $"{p.Vehicle?.Brand} {p.Vehicle?.Model}",
                CustomerName = $"{p.Customer?.FirstName} {p.Customer?.LastName}",
                InsuranceCompanyName = p.InsuranceCompany?.CompanyName ?? "N/A",
                InsuranceType = p.InsuranceType,
                EndDate = p.EndDate,
                DaysUntilExpiration = p.DaysUntilExpiration()
            }).ToList();

            return new GetExpiringPoliciesResponse
            {
                Policies = policiesDto,
                TotalCount = policiesDto.Count
            };
        }
    }
}

