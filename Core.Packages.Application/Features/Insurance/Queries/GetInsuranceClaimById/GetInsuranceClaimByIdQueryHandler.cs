using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimById
{
    public class GetInsuranceClaimByIdQueryHandler : IRequestHandler<GetInsuranceClaimByIdQuery, GetInsuranceClaimByIdResponse>
    {
        private readonly IInsuranceClaimRepository _insuranceClaimRepository;
        private readonly ITenantService _tenantService;

        public GetInsuranceClaimByIdQueryHandler(
            IInsuranceClaimRepository insuranceClaimRepository,
            ITenantService tenantService)
        {
            _insuranceClaimRepository = insuranceClaimRepository;
            _tenantService = tenantService;
        }

        public async Task<GetInsuranceClaimByIdResponse> Handle(GetInsuranceClaimByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var claim = await _insuranceClaimRepository.GetWithDetailsAsync(request.Id, cancellationToken);
            if (claim == null || claim.ClientId != clientId)
            {
                throw new DomainException("INSURANCE_CLAIM_NOT_FOUND", new { ClaimId = request.Id });
            }

            return new GetInsuranceClaimByIdResponse
            {
                Id = claim.Id,
                ClaimNumber = claim.ClaimNumber,
                WorkOrderId = claim.WorkOrderId,
                WorkOrderNumber = claim.WorkOrder?.WorkOrderNumber ?? "N/A",
                InsurancePolicyId = claim.InsurancePolicyId,
                PolicyNumber = claim.InsurancePolicy?.PolicyNumber ?? "N/A",
                InsuranceCompanyName = claim.InsurancePolicy?.InsuranceCompany?.CompanyName ?? "N/A",
                DamageDate = claim.DamageDate,
                DamageDescription = claim.DamageDescription,
                DamageAmount = claim.DamageAmount,
                ApprovedAmount = claim.ApprovedAmount,
                DeductibleAmount = claim.DeductibleAmount,
                PayableAmount = claim.PayableAmount,
                Status = claim.Status,
                ApprovalDate = claim.ApprovalDate,
                PaymentDate = claim.PaymentDate,
                RejectionReason = claim.RejectionReason,
                Photos = claim.Photos,
                Notes = claim.Notes
            };
        }
    }
}
