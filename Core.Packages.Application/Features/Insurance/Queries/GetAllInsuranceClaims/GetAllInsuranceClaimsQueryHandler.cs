using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceClaims
{
    public class GetAllInsuranceClaimsQueryHandler : IRequestHandler<GetAllInsuranceClaimsQuery, GetAllInsuranceClaimsResponse>
    {
        private readonly IInsuranceClaimRepository _insuranceClaimRepository;
        private readonly ITenantService _tenantService;

        public GetAllInsuranceClaimsQueryHandler(
            IInsuranceClaimRepository insuranceClaimRepository,
            ITenantService tenantService)
        {
            _insuranceClaimRepository = insuranceClaimRepository;
            _tenantService = tenantService;
        }

        public async Task<GetAllInsuranceClaimsResponse> Handle(GetAllInsuranceClaimsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var claims = await _insuranceClaimRepository.Query()
                .Include(c => c.InsurancePolicy)
                    .ThenInclude(p => p != null ? p.InsuranceCompany : null)
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.DamageDate)
                .ToListAsync(cancellationToken);

            var claimsDto = claims.Select(c => new InsuranceClaimDto
            {
                Id = c.Id,
                ClaimNumber = c.ClaimNumber,
                InsurancePolicyId = c.InsurancePolicyId,
                PolicyNumber = c.InsurancePolicy?.PolicyNumber ?? "N/A",
                InsuranceCompanyName = c.InsurancePolicy?.InsuranceCompany?.CompanyName ?? "N/A",
                DamageDate = c.DamageDate,
                DamageDescription = c.DamageDescription,
                DamageAmount = c.DamageAmount,
                ApprovedAmount = c.ApprovedAmount,
                DeductibleAmount = c.DeductibleAmount,
                PayableAmount = c.PayableAmount,
                Status = c.Status,
                ApprovalDate = c.ApprovalDate,
                PaymentDate = c.PaymentDate,
                RejectionReason = c.RejectionReason,
                ClientId = c.ClientId
            }).ToList();

            return new GetAllInsuranceClaimsResponse
            {
                Claims = claimsDto
            };
        }
    }
}
