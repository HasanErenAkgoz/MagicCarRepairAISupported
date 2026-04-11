using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByCustomer
{
    public class GetInsuranceClaimsByCustomerResponse
    {
        public List<InsuranceClaimDto> Claims { get; set; } = new();
    }
}
