using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.RenewInsurancePolicy
{
    public class RenewInsurancePolicyCommandHandler : IRequestHandler<RenewInsurancePolicyCommand, RenewInsurancePolicyResponse>
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public RenewInsurancePolicyCommandHandler(
            IInsurancePolicyRepository insurancePolicyRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<RenewInsurancePolicyResponse> Handle(RenewInsurancePolicyCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var existingPolicy = await _insurancePolicyRepository.GetByIdAsync(request.PolicyId);
            if (existingPolicy == null || existingPolicy.ClientId != clientId)
            {
                throw new DomainException("INSURANCE_POLICY_NOT_FOUND", new { PolicyId = request.PolicyId });
            }

            // Validate new end date
            if (request.NewEndDate <= existingPolicy.EndDate)
            {
                throw new DomainException("NEW_END_DATE_MUST_BE_AFTER_CURRENT_END_DATE", new { CurrentEndDate = existingPolicy.EndDate, NewEndDate = request.NewEndDate });
            }

            var oldPolicyNumber = existingPolicy.PolicyNumber;
            
            // Update existing policy status to Expired or Inactive
            // Note: Using Expired since Renewed status doesn't exist. In production, consider adding Renewed status.
            existingPolicy.Status = InsuranceStatus.Expired;
            _insurancePolicyRepository.Update(existingPolicy);

            // Create new policy with renewal suffix
            var newPolicyNumber = $"{oldPolicyNumber}-REN";
            var year = DateTime.UtcNow.Year;
            var policyNumberWithYear = $"{newPolicyNumber}-{year}";

            // Check if policy number already exists (rare case)
            var existingRenewal = await _insurancePolicyRepository.GetByPolicyNumberAsync(policyNumberWithYear, cancellationToken);
            if (existingRenewal != null)
            {
                // Add timestamp to make it unique
                policyNumberWithYear = $"{newPolicyNumber}-{year}-{DateTime.UtcNow:HHmmss}";
            }

            var newPolicy = new InsurancePolicy
            {
                PolicyNumber = policyNumberWithYear,
                VehicleId = existingPolicy.VehicleId,
                CustomerId = existingPolicy.CustomerId,
                InsuranceCompanyId = existingPolicy.InsuranceCompanyId,
                InsuranceType = existingPolicy.InsuranceType,
                StartDate = existingPolicy.EndDate.AddDays(1), // Start from day after old policy ends
                EndDate = request.NewEndDate,
                PremiumAmount = request.NewPremiumAmount ?? existingPolicy.PremiumAmount,
                CoverageAmount = existingPolicy.CoverageAmount,
                DeductiblePercentage = existingPolicy.DeductiblePercentage,
                DeductibleAmount = request.NewDeductibleAmount ?? existingPolicy.DeductibleAmount,
                PolicyFileId = existingPolicy.PolicyFileId,
                Notes = $"Poliçe yenilendi. Eski poliçe no: {oldPolicyNumber}",
                Status = InsuranceStatus.Active,
                ClientId = clientId
            };

            await _insurancePolicyRepository.AddAsync(newPolicy, cancellationToken);
            await _insurancePolicyRepository.SaveChangesAsync();

            var response = _mapper.Map<RenewInsurancePolicyResponse>(newPolicy);
            response.OldPolicyNumber = oldPolicyNumber;
            return response;
        }
    }
}
