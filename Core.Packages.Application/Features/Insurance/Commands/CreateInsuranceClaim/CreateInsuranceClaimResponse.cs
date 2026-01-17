using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceClaim
{
    public class CreateInsuranceClaimResponse
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public int? WorkOrderId { get; set; }
        public int InsurancePolicyId { get; set; }
        public DateTime DamageDate { get; set; }
        public decimal DamageAmount { get; set; }
        public ClaimStatus Status { get; set; }
    }
}

