using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.DeactivateClient
{
    public class DeactivateClientCommand : IRequest<IDataResult<DeactivateClientResponse>>
    {
        public int ClientId { get; set; }
    }

    public class DeactivateClientResponse
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
