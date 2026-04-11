using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder
{
    public class GetInsuranceClaimsByWorkOrderQueryHandler : IRequestHandler<GetInsuranceClaimsByWorkOrderQuery, GetInsuranceClaimsByWorkOrderResponse>
    {
        private readonly IInsuranceClaimRepository _insuranceClaimRepository;
        private readonly ITenantService _tenantService;

        public GetInsuranceClaimsByWorkOrderQueryHandler(
            IInsuranceClaimRepository insuranceClaimRepository,
            ITenantService tenantService)
        {
            _insuranceClaimRepository = insuranceClaimRepository;
            _tenantService = tenantService;
        }

        public async Task<GetInsuranceClaimsByWorkOrderResponse> Handle(GetInsuranceClaimsByWorkOrderQuery request, CancellationToken cancellationToken)
        {
            var claims = await _insuranceClaimRepository.GetByWorkOrderIdAsync(request.WorkOrderId, cancellationToken);

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

            return new GetInsuranceClaimsByWorkOrderResponse
            {
                Claims = claimsDto
            };
        }
    }
}
