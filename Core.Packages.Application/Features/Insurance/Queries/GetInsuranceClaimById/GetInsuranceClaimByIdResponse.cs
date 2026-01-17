using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimById
{
    public class GetInsuranceClaimByIdResponse
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public int? WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public int InsurancePolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public string InsuranceCompanyName { get; set; } = string.Empty;
        public DateTime DamageDate { get; set; }
        public string DamageDescription { get; set; } = string.Empty;
        public decimal DamageAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? DeductibleAmount { get; set; }
        public decimal? PayableAmount { get; set; }
        public ClaimStatus Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? RejectionReason { get; set; }
        public string? Photos { get; set; }
        public string? Notes { get; set; }
    }
}
