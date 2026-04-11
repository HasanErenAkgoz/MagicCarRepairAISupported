using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.UseRoles.Commands.RemoveFromUser
{
    public class RemoveRoleFromUserCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        /// <summary>
        /// SystemAdmin kullanımı için opsiyonel clientId override.
        /// </summary>
        public int? ClientId { get; set; }
    }
}
