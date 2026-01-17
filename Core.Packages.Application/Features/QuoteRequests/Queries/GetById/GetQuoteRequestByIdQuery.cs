using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetById
{
    public class GetQuoteRequestByIdQuery : IRequest<IDataResult<GetQuoteRequestByIdResponse>>
    {
        public int Id { get; set; }
    }
}

