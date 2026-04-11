using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest<IDataResult<DeleteClientResponse>>
    {
        public int ClientId { get; set; }
    }

    public class DeleteClientResponse
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
