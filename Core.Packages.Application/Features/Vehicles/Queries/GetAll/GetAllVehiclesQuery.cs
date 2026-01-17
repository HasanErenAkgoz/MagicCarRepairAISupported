using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetAll
{
    public class GetAllVehiclesQuery : IRequest<List<GetAllVehiclesResponse>>
    {
        public int? CustomerId { get; set; } // Müşteriye göre filtreleme
        public string? SearchTerm { get; set; } // Plaka, Marka, Model ile arama
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

