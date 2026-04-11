using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceClaims
{
    public class GetAllInsuranceClaimsResponse
    {
        public List<InsuranceClaimDto> Claims { get; set; } = new();
    }
}
