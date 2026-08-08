using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetExpiringPolicies
{
    public class GetExpiringPoliciesResponse
    {
        public List<ExpiringPolicyDto> Policies { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class ExpiringPolicyDto
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string VehicleLicensePlate { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
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
    }
}

