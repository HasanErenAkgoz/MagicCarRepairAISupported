using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicPortfolio
{
    public class GetPublicPortfolioQuery : IRequest<IDataResult<List<GetPublicPortfolioResponse>>>
    {
        public int ClientId { get; set; }
        public string? Category { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
