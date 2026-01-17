using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceClaim
{
    public class CreateInsuranceClaimCommandHandler : IRequestHandler<CreateInsuranceClaimCommand, CreateInsuranceClaimResponse>
    {
        private readonly IInsuranceClaimRepository _insuranceClaimRepository;
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateInsuranceClaimCommandHandler(
            IInsuranceClaimRepository insuranceClaimRepository,
            IInsurancePolicyRepository insurancePolicyRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _insuranceClaimRepository = insuranceClaimRepository;
            _insurancePolicyRepository = insurancePolicyRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateInsuranceClaimResponse> Handle(CreateInsuranceClaimCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Validate claim number uniqueness
            var existingClaim = await _insuranceClaimRepository.GetByClaimNumberAsync(request.ClaimNumber, cancellationToken);
            if (existingClaim != null)
            {
                throw new DomainException("INSURANCE_CLAIM_NUMBER_ALREADY_EXISTS", new { ClaimNumber = request.ClaimNumber });
            }

            // Validate insurance policy
            var policy = await _insurancePolicyRepository.GetByIdAsync(request.InsurancePolicyId);
            if (policy == null || policy.ClientId != clientId)
            {
                throw new DomainException("INSURANCE_POLICY_NOT_FOUND", new { InsurancePolicyId = request.InsurancePolicyId });
            }

            // Check if policy is valid (not expired)
            if (!policy.IsValid())
            {
                throw new DomainException("INSURANCE_POLICY_EXPIRED_OR_INVALID", new { PolicyNumber = policy.PolicyNumber });
            }

            // Validate WorkOrder if provided
            if (request.WorkOrderId.HasValue)
            {
                var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId.Value);
                if (workOrder == null || workOrder.ClientId != clientId)
                {
                    throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId.Value });
                }

                // Validate that WorkOrder's VehicleId matches Policy's VehicleId
                if (policy.VehicleId != workOrder.VehicleId)
                {
                    throw new DomainException("WORKORDER_VEHICLE_MISMATCH", new { 
                        WorkOrderId = request.WorkOrderId.Value,
                        WorkOrderVehicleId = workOrder.VehicleId,
                        PolicyVehicleId = policy.VehicleId
                    });
                }

                // Check if claim already exists for this work order
                var existingWorkOrderClaim = await _insuranceClaimRepository.GetByWorkOrderIdAsync(request.WorkOrderId.Value, cancellationToken);
                if (existingWorkOrderClaim.Any(c => c.Status != ClaimStatus.Cancelled))
                {
                    throw new DomainException("INSURANCE_CLAIM_ALREADY_EXISTS_FOR_WORKORDER", new { WorkOrderId = request.WorkOrderId.Value });
                }
            }

            var claim = new InsuranceClaim
            {
                ClaimNumber = request.ClaimNumber,
                WorkOrderId = request.WorkOrderId,
                InsurancePolicyId = request.InsurancePolicyId,
                DamageDate = request.DamageDate,
                DamageDescription = request.DamageDescription,
                DamageAmount = request.DamageAmount,
                Photos = request.Photos,
                Notes = request.Notes,
                Status = ClaimStatus.Applied,
                ClientId = clientId
            };

            // Load policy to calculate deductible
            claim.InsurancePolicy = policy;
            claim.CalculateDeductible();

            await _insuranceClaimRepository.AddAsync(claim, cancellationToken);

            return _mapper.Map<CreateInsuranceClaimResponse>(claim);
        }
    }
}

