using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByCustomer
{
    public class GetInsuranceClaimsByCustomerQueryHandler
        : IRequestHandler<GetInsuranceClaimsByCustomerQuery, GetInsuranceClaimsByCustomerResponse>
    {
        private readonly IInsuranceClaimRepository _insuranceClaimRepository;
        private readonly ITenantService _tenantService;

        public GetInsuranceClaimsByCustomerQueryHandler(
            IInsuranceClaimRepository insuranceClaimRepository,
            ITenantService tenantService)
        {
            _insuranceClaimRepository = insuranceClaimRepository;
            _tenantService = tenantService;
        }

        public async Task<GetInsuranceClaimsByCustomerResponse> Handle(
            GetInsuranceClaimsByCustomerQuery request,
            CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var claims = await _insuranceClaimRepository.GetByCustomerIdAsync(
                request.CustomerId, cancellationToken);

            var claimsDto = claims
                .Where(c => c.ClientId == clientId)
                .Select(c => new InsuranceClaimDto
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

            return new GetInsuranceClaimsByCustomerResponse { Claims = claimsDto };
        }
    }
}
