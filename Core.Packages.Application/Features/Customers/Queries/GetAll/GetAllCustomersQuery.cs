using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersQuery : IRequest<List<GetAllCustomersResponse>>
    {
        public string? SearchTerm { get; set; } // Ad, Soyad, Email, Telefon, TC ile arama
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

