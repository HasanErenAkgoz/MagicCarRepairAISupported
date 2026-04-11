using MagicCarRepairAISupported.Application.Common.Models.JWT;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.ImpersonateClient
{
    public class ImpersonateClientCommand : IRequest<IDataResult<AccessToken>>
    {
        public int ClientId { get; set; }
    }
}
