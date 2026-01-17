using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByCustomer
{
    public class GetInsurancePoliciesByCustomerQuery : IRequest<GetInsurancePoliciesByCustomerResponse>
    {
        public int CustomerId { get; set; }
        public bool? ActiveOnly { get; set; }
    }
}
