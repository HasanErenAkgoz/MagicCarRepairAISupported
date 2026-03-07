using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.ApproveClient
{
    public class ApproveClientCommand : IRequest<IDataResult<ApproveClientResponse>>
    {
        public int ClientId { get; set; }
    }

    public class ApproveClientResponse
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
