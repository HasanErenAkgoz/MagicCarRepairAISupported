using MagicCarRepairAISupported.Application.Features.Users.Commands.CreateUserByAdmin;
using MagicCarRepairAISupported.Application.Features.Users.Commands.DeleteUser;
using MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserProfile;
using MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserRole;
using MagicCarRepairAISupported.Application.Features.Users.Queries.GetAllUsers;
using MagicCarRepairAISupported.Application.Features.Users.Queries.GetMyProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    /// <summary>
    /// User Controller — Kullanıcı profil ve yönetim endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/user")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private bool IsSystemAdmin()
            => User.FindFirst("UserType")?.Value == "1";

        private bool IsManager()
            => User.FindFirst("UserType")?.Value == "2";

        private int? GetCallerClientId()
        {
            var raw = User.FindFirst("ClientId")?.Value;
            return int.TryParse(raw, out var id) ? id : null;
        }

        // ──────────────────────────────────────────────
        // Genel (herkese açık)
        // ──────────────────────────────────────────────

        /// <summary>
        /// Kullanıcının profil bilgilerini getirir
        /// </summary>
        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }

        /// <summary>
        /// Kullanıcının kendi profil bilgilerini günceller (Admin, Manager, Employee)
        /// </summary>
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateUserProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // ──────────────────────────────────────────────
        // Sadece SystemAdmin
        // ──────────────────────────────────────────────

        /// <summary>
        /// Kullanıcıları listeler — SystemAdmin tümünü, Manager kendi clientId'sine göre görebilir
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] int? clientId = null,
            [FromQuery] int? userType = null)
        {
            if (IsSystemAdmin())
            {
                // SystemAdmin: query param'daki clientId'yi kullan
                var query = new GetAllUsersQuery { ClientId = clientId, UserType = userType };
                var result = await _mediator.Send(query);
                return Ok(result);
            }

            if (IsManager())
            {
                // Manager: yalnızca kendi clientId'si
                var managerClientId = GetCallerClientId();
                if (managerClientId == null) return Forbid();

                var query = new GetAllUsersQuery { ClientId = managerClientId, UserType = userType };
                var result = await _mediator.Send(query);
                return Ok(result);
            }

            return Forbid();
        }

        /// <summary>
        /// Yeni kullanıcı oluşturur — SystemAdmin veya Manager (kendi clientId'si için)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserByAdminRequest body)
        {
            int resolvedClientId;

            if (IsSystemAdmin())
            {
                resolvedClientId = body.ClientId;
            }
            else if (IsManager())
            {
                var managerClientId = GetCallerClientId();
                if (managerClientId == null) return Forbid();
                resolvedClientId = managerClientId.Value;
            }
            else
            {
                return Forbid();
            }

            var command = new CreateUserByAdminCommand
            {
                FirstName = body.FirstName,
                LastName = body.LastName,
                Email = body.Email,
                PhoneNumber = body.PhoneNumber,
                Password = body.Password,
                UserType = body.UserType,
                ClientId = resolvedClientId
            };

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Belirli kullanıcının rolünü günceller — SystemAdmin veya Manager (kendi clientId kullanıcıları için)
        /// </summary>
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleRequest body)
        {
            if (!IsSystemAdmin() && !IsManager())
                return Forbid();

            if (IsManager())
            {
                // Manager yalnızca kendi clientId'sindeki kullanıcıların rolünü değiştirebilir
                var managerClientId = GetCallerClientId();
                if (managerClientId == null) return Forbid();

                var command = new UpdateUserRoleCommand { UserId = id, UserType = body.UserType, CallerClientId = managerClientId };
                var result = await _mediator.Send(command);
                if (!result.Success) return BadRequest(result);
                return Ok(result);
            }
            else
            {
                var command = new UpdateUserRoleCommand { UserId = id, UserType = body.UserType };
                var result = await _mediator.Send(command);
                if (!result.Success) return BadRequest(result);
                return Ok(result);
            }
        }

        /// <summary>
        /// Kullanıcıyı tüm verileriyle siler — SystemAdmin ve Manager erişebilir
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!IsSystemAdmin() && !IsManager())
                return Forbid();

            var command = new DeleteUserCommand { UserId = id };
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }

    public record UpdateUserRoleRequest(int UserType);
    public record CreateUserByAdminRequest(
        string FirstName,
        string LastName,
        string Email,
        string? PhoneNumber,
        string Password,
        int UserType,
        int ClientId);
}
