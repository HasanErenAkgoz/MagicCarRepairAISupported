using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceClaimStatus
{
    public class UpdateInsuranceClaimStatusResponse
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public ClaimStatus Status { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? PayableAmount { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
