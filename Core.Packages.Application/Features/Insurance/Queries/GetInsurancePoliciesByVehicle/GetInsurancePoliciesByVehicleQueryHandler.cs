using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByVehicle
{
    public class GetInsurancePoliciesByVehicleQueryHandler : IRequestHandler<GetInsurancePoliciesByVehicleQuery, GetInsurancePoliciesByVehicleResponse>
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly ITenantService _tenantService;

        public GetInsurancePoliciesByVehicleQueryHandler(
            IInsurancePolicyRepository insurancePolicyRepository,
            ITenantService tenantService)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
            _tenantService = tenantService;
        }

        public async Task<GetInsurancePoliciesByVehicleResponse> Handle(GetInsurancePoliciesByVehicleQuery request, CancellationToken cancellationToken)
        {
            var policies = await _insurancePolicyRepository.GetByVehicleIdAsync(request.VehicleId, cancellationToken);

            var policiesDto = policies.Select(p => new InsurancePolicyDto
            {
                Id = p.Id,
                PolicyNumber = p.PolicyNumber,
                InsuranceCompanyName = p.InsuranceCompany?.CompanyName ?? "N/A",
                InsuranceType = p.InsuranceType,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                PremiumAmount = p.PremiumAmount,
                DeductiblePercentage = p.DeductiblePercentage,
                DeductibleAmount = p.DeductibleAmount,
                Status = p.Status,
                DaysUntilExpiration = p.DaysUntilExpiration()
            }).ToList();

            return new GetInsurancePoliciesByVehicleResponse
            {
                Policies = policiesDto
            };
        }
    }
}
