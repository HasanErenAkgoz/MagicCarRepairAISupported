using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceClaim
{
    public class CreateInsuranceClaimCommand : IRequest<CreateInsuranceClaimResponse>
    {
        public string ClaimNumber { get; set; } = string.Empty;
        public int? WorkOrderId { get; set; }
        public int InsurancePolicyId { get; set; }
        public DateTime DamageDate { get; set; }
        public string DamageDescription { get; set; } = string.Empty;
        public decimal DamageAmount { get; set; }
        public string? Photos { get; set; } // JSON formatında file paths
        public string? Notes { get; set; }
    }
}

