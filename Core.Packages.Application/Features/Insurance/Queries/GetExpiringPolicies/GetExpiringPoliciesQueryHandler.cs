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
                VehicleId = p.VehicleId,
                VehicleBrand = p.Vehicle?.Brand ?? string.Empty,
                VehicleModel = p.Vehicle?.Model ?? string.Empty,
                VehicleLicensePlate = p.Vehicle?.LicensePlate ?? string.Empty,
                CustomerId = p.CustomerId,
                CustomerName = p.Customer != null ? $"{p.Customer.FirstName} {p.Customer.LastName}" : string.Empty,
                InsuranceCompanyName = p.InsuranceCompany?.CompanyName ?? string.Empty,
                InsuranceType = p.InsuranceType,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                PremiumAmount = p.PremiumAmount,
                CoverageAmount = p.CoverageAmount,
                DeductiblePercentage = p.DeductiblePercentage,
                DeductibleAmount = p.DeductibleAmount,
                Status = p.Status,
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

