using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceClaimStatus
{
    public class UpdateInsuranceClaimStatusCommand : IRequest<UpdateInsuranceClaimStatusResponse>
    {
        public int ClaimId { get; set; }
        public ClaimStatus Status { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string? RejectionReason { get; set; }
        public string? Notes { get; set; }
    }
}
