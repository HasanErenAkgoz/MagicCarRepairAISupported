using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.RolePermissions.Delete
{
    public class DeleteRolePermissionCommand : IRequest<IResult>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        /// <summary>
        /// SystemAdmin kullanımı için opsiyonel clientId override.
        /// </summary>
        public int? ClientId { get; set; }
    }
}
