using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.RolePermissions.Create
{
    public class CreateRolePermissionCommand : IRequest<IResult>
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
    }
}
