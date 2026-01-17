using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMaintenanceHistory
{
    public class GetMaintenanceHistoryQuery : IRequest<List<GetMaintenanceHistoryResponse>>
    {
        public int? VehicleId { get; set; }
    }
}






