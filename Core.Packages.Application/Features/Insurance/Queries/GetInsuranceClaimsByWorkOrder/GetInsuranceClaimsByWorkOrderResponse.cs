using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder
{
    public class GetInsuranceClaimsByWorkOrderResponse
    {
        public List<InsuranceClaimDto> Claims { get; set; } = new();
    }

    public class InsuranceClaimDto
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;
        public string InsuranceCompanyName { get; set; } = string.Empty;
        public DateTime DamageDate { get; set; }
        public decimal DamageAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? PayableAmount { get; set; }
        public ClaimStatus Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
