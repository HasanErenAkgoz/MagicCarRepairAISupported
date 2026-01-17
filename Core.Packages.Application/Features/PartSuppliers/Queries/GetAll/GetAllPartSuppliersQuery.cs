using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetAll
{
    public class GetAllPartSuppliersQuery : IRequest<GetAllPartSuppliersResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SearchTerm { get; set; } // CompanyName, ContactPerson, Phone, Email'de arama
        public bool? IsActiveOnly { get; set; } // Sadece aktif tedarikçiler
    }
}

