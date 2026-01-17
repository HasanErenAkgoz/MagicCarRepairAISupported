using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsurancePolicy
{
    public class CreateInsurancePolicyResponse
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public int InsuranceCompanyId { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public InsuranceStatus Status { get; set; }
    }
}

