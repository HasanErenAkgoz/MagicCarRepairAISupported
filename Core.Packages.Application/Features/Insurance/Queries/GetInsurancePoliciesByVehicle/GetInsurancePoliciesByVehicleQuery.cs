using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByVehicle
{
    public class GetInsurancePoliciesByVehicleQuery : IRequest<GetInsurancePoliciesByVehicleResponse>
    {
        public int VehicleId { get; set; }
    }
}
