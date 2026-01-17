using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.RenewInsurancePolicy
{
    public class RenewInsurancePolicyResponse
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public string OldPolicyNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public InsuranceStatus Status { get; set; }
    }
}
