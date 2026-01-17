using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.RenewInsurancePolicy
{
    public class RenewInsurancePolicyCommand : IRequest<RenewInsurancePolicyResponse>
    {
        public int PolicyId { get; set; }
        public DateTime NewEndDate { get; set; }
        public decimal? NewPremiumAmount { get; set; }
        public decimal? NewDeductibleAmount { get; set; }
        public decimal? NewDeductiblePercentage { get; set; }
    }
}
