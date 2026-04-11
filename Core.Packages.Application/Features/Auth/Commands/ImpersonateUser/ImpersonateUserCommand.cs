using MagicCarRepairAISupported.Application.Common.Models.JWT;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.ImpersonateUser
{
    public class ImpersonateUserCommand : IRequest<IDataResult<AccessToken>>
    {
        public int TargetUserId { get; set; }
    }
}
