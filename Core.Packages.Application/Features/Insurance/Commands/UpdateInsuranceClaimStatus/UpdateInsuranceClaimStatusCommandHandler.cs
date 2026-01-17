using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceClaimStatus
{
    public class UpdateInsuranceClaimStatusCommandHandler : IRequestHandler<UpdateInsuranceClaimStatusCommand, UpdateInsuranceClaimStatusResponse>
    {
        private readonly IInsuranceClaimRepository _insuranceClaimRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateInsuranceClaimStatusCommandHandler(
            IInsuranceClaimRepository insuranceClaimRepository,
            ITenantService tenantService,
            IMapper mapper,
            IMediator mediator)
        {
            _insuranceClaimRepository = insuranceClaimRepository;
            _tenantService = tenantService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UpdateInsuranceClaimStatusResponse> Handle(UpdateInsuranceClaimStatusCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var claim = await _insuranceClaimRepository.GetWithDetailsAsync(request.ClaimId, cancellationToken);
            if (claim == null || claim.ClientId != clientId)
            {
                throw new DomainException("INSURANCE_CLAIM_NOT_FOUND", new { ClaimId = request.ClaimId });
            }

            // Status değişikliğine göre işlem yap
            switch (request.Status)
            {
                case ClaimStatus.Approved:
                    if (!request.ApprovedAmount.HasValue || request.ApprovedAmount.Value <= 0)
                    {
                        throw new DomainException("APPROVED_AMOUNT_REQUIRED_FOR_APPROVAL");
                    }
                    claim.Approve(request.ApprovedAmount.Value);
                    break;

                case ClaimStatus.Rejected:
                    if (string.IsNullOrWhiteSpace(request.RejectionReason))
                    {
                        throw new DomainException("REJECTION_REASON_REQUIRED");
                    }
                    claim.Reject(request.RejectionReason);
                    break;

                case ClaimStatus.Paid:
                    if (claim.Status != ClaimStatus.Approved)
                    {
                        throw new DomainException("CLAIM_MUST_BE_APPROVED_BEFORE_PAYMENT");
                    }
                    claim.MarkAsPaid();

                    // Sigorta ödemesi yapıldığında, eğer WorkOrder yoksa otomatik oluştur
                    if (!claim.WorkOrderId.HasValue && claim.InsurancePolicy != null)
                    {
                        var createWorkOrderCommand = new CreateWorkOrderCommand
                        {
                            VehicleId = claim.InsurancePolicy.VehicleId,
                            CustomerId = claim.InsurancePolicy.CustomerId,
                            EntryDate = DateTime.UtcNow,
                            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(7), // Varsayılan 7 gün
                            Priority = WorkOrderPriority.Normal,
                            CustomerComplaints = $"Sigorta hasarı: {claim.DamageDescription}",
                            Notes = $"Sigorta hasar dosyası #{claim.ClaimNumber} için otomatik oluşturuldu. Onaylanan tutar: {claim.ApprovedAmount:N2} TL"
                        };

                        var workOrderResponse = await _mediator.Send(createWorkOrderCommand, cancellationToken);
                        claim.WorkOrderId = workOrderResponse.Id;
                    }
                    break;

                case ClaimStatus.UnderReview:
                    claim.Status = ClaimStatus.UnderReview;
                    break;

                default:
                    claim.Status = request.Status;
                    break;
            }

            if (!string.IsNullOrWhiteSpace(request.Notes))
            {
                claim.Notes = request.Notes;
            }

            _insuranceClaimRepository.Update(claim);
            await _insuranceClaimRepository.SaveChangesAsync();

            return _mapper.Map<UpdateInsuranceClaimStatusResponse>(claim);
        }
    }
}
