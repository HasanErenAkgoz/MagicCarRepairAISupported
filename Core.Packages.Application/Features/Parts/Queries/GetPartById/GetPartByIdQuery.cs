using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById
{
    public class GetPartByIdQuery : IRequest<GetPartByIdResponse>
    {
        public int Id { get; set; }
    }
}

