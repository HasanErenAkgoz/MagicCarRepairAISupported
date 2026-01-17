using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetAll
{
    public class GetAllQuoteRequestsQuery : IRequest<IDataResult<List<GetAllQuoteRequestsResponse>>>
    {
        public QuoteStatus? Status { get; set; }
        public QuoteRequestType? RequestType { get; set; }
        public int? CustomerId { get; set; }
        public int? ClientId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

