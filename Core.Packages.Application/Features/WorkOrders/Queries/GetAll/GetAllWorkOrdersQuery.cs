using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll
{
    public class GetAllWorkOrdersQuery : IRequest<List<GetAllWorkOrdersResponse>>
    {
        public WorkOrderStatus? Status { get; set; }
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

