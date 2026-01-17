using MagicCarRepairAISupported.Application.Common.Attributies;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts
{
    [Cache("parts:list:ClientId:{ClientId}:PageNumber:{PageNumber}:PageSize:{PageSize}:SearchTerm:{SearchTerm}:Category:{Category}:BrandType:{BrandType}:LowStockOnly:{LowStockOnly}", 15, true)]
    public class GetAllPartsQuery : IRequest<GetAllPartsResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public PartCategory? Category { get; set; }
        public PartBrandType? BrandType { get; set; }
        public bool? LowStockOnly { get; set; }
    }
}

