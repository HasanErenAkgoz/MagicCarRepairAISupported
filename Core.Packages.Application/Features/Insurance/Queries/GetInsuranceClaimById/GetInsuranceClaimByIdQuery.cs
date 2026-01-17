using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimById
{
    public class GetInsuranceClaimByIdQuery : IRequest<GetInsuranceClaimByIdResponse>
    {
        public int Id { get; set; }
    }
}
