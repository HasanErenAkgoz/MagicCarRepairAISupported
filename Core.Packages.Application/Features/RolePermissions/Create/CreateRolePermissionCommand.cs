using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.RolePermissions.Create
{
    public class CreateRolePermissionCommand : IRequest<IResult>
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

        /// <summary>
        /// SystemAdmin kullanımı için opsiyonel clientId override.
        /// Null ise tenant servisinden alınır.
        /// </summary>
        public int? ClientId { get; set; }
    }
}
