using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetOpen
{
    public class GetOpenQuoteRequestsQuery : IRequest<IDataResult<List<GetOpenQuoteRequestsResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

