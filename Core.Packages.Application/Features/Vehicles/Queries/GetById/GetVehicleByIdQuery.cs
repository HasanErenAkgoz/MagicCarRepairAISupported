using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetById
{
    public class GetVehicleByIdQuery : IRequest<GetVehicleByIdResponse>
    {
        public int Id { get; set; }
    }
}

