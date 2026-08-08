using MagicCarRepairAISupported.Application.Features.Roles.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Roles.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.Roles.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.RolePermissions.Create;
using MagicCarRepairAISupported.Application.Features.RolePermissions.Delete;
using MagicCarRepairAISupported.Application.Features.UseRoles.Commands.AssignToUser;
using MagicCarRepairAISupported.Application.Features.UseRoles.Commands.RemoveFromUser;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseApiController
    {
        /// <summary>
        /// Rolleri listeler. clientId parametresi SystemAdmin için belirli bir client'ın rollerini getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? clientId)
        {
            var result = await Mediator.Send(new GetAllRolesQuery { ClientId = clientId });
            return GetResponse(result);
        }

        /// <summary>
        /// Yeni rol oluşturur.
        /// </summary>
        [HttpPost("Role")]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand createRoleCommand)
        {
            var result = await Mediator.Send(createRoleCommand);
            return GetResponse(result);
        }

        /// <summary>
        /// Rolü siler. clientId parametresi SystemAdmin kullanımı içindir.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int? clientId)
        {
            var result = await Mediator.Send(new DeleteRoleCommand { RoleId = id, ClientId = clientId });
            return GetResponse(result);
        }

        /// <summary>
        /// Role izin ekler.
        /// </summary>
        [HttpPost("{id}/permissions/{permissionId}")]
        public async Task<IActionResult> AddPermission(int id, int permissionId, [FromQuery] int? clientId)
        {
            var result = await Mediator.Send(new CreateRolePermissionCommand
            {
                RoleId = id,
                PermissionId = permissionId,
                ClientId = clientId,
            });
            return GetResponse(result);
        }

        /// <summary>
        /// Rolden izin kaldırır.
        /// </summary>
        [HttpDelete("{id}/permissions/{permissionId}")]
        public async Task<IActionResult> RemovePermission(int id, int permissionId, [FromQuery] int? clientId)
        {
            var result = await Mediator.Send(new DeleteRolePermissionCommand
            {
                RoleId = id,
                PermissionId = permissionId,
                ClientId = clientId,
            });
            return GetResponse(result);
        }

        /// <summary>
        /// Kullanıcıya rol atar.
        /// </summary>
        [HttpPost("{id}/users/{userId}")]
        public async Task<IActionResult> AssignToUser(int id, int userId, [FromQuery] int? clientId)
        {
            var result = await Mediator.Send(new AssignRoleToUserCommand
            {
                RoleId = id,
                UserId = userId,
                ClientId = clientId,
            });
            return GetResponse(result);
        }

        /// <summary>
        /// Kullanıcıdan rol kaldırır.
        /// </summary>
        [HttpDelete("{id}/users/{userId}")]
        public async Task<IActionResult> RemoveFromUser(int id, int userId, [FromQuery] int? clientId)
        {
            var result = await Mediator.Send(new RemoveRoleFromUserCommand
            {
                RoleId = id,
                UserId = userId,
                ClientId = clientId,
            });
            return GetResponse(result);
        }
    }
}
