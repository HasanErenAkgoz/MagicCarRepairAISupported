using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder
{
    public class GetInsuranceClaimsByWorkOrderQuery : IRequest<GetInsuranceClaimsByWorkOrderResponse>
    {
        public int WorkOrderId { get; set; }
    }
}
