using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByCustomer
{
    public class GetInsurancePoliciesByCustomerResponse
    {
        public List<InsurancePolicyDto> Policies { get; set; } = new();
    }

    public class InsurancePolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string VehicleLicensePlate { get; set; } = string.Empty;
        public int InsuranceCompanyId { get; set; }
        public string InsuranceCompanyName { get; set; } = string.Empty;
        public InsuranceType InsuranceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal? CoverageAmount { get; set; }
        public decimal DeductiblePercentage { get; set; }
        public decimal? DeductibleAmount { get; set; }
        public InsuranceStatus Status { get; set; }
        public int DaysUntilExpiration { get; set; }
        public bool IsValid { get; set; }
    }
}
