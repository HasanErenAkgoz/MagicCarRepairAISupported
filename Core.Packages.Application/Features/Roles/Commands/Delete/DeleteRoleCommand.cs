using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<IResult>
    {
        public int RoleId { get; set; }

        /// <summary>
        /// SystemAdmin kullanımı için opsiyonel clientId override.
        /// Null ise tenant servisinden alınır.
        /// </summary>
        public int? ClientId { get; set; }
    }
}
