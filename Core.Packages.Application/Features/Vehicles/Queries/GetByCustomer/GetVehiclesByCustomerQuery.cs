using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetByCustomer
{
    public class GetVehiclesByCustomerQuery : IRequest<List<GetVehiclesByCustomerResponse>>
    {
        public int CustomerId { get; set; }
    }
}

