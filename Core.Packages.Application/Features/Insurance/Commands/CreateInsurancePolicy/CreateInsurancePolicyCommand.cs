using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsurancePolicy
{
    public class CreateInsurancePolicyCommand : IRequest<CreateInsurancePolicyResponse>
    {
        public string PolicyNumber { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public int InsuranceCompanyId { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal? CoverageAmount { get; set; }
        public decimal DeductiblePercentage { get; set; } = 0;
        public decimal? DeductibleAmount { get; set; }
        public int? PolicyFileId { get; set; }
        public string? Notes { get; set; }
    }
}

