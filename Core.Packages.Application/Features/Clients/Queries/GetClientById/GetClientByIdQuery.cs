using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<IDataResult<GetClientByIdResponse>>
    {
        public int Id { get; set; }
    }
}

