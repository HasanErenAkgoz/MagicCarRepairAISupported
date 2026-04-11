using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByCustomer
{
    public class GetInsuranceClaimsByCustomerQuery : IRequest<GetInsuranceClaimsByCustomerResponse>
    {
        public int CustomerId { get; set; }
    }
}
