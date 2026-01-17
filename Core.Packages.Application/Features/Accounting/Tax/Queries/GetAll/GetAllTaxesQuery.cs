using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetAll
{
    public class GetAllTaxesQuery : IRequest<GetAllTaxesResponse>
    {
        public TaxType? TaxType { get; set; }
        public TaxStatus? Status { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public bool? OverdueOnly { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
