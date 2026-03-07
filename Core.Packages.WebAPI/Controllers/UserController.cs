using MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserRole;
using MagicCarRepairAISupported.Application.Features.Users.Queries.GetAllUsers;
using MagicCarRepairAISupported.Application.Features.Users.Queries.GetMyProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    /// <summary>
    /// User Controller — Kullanıcı profil ve yönetim endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Giriş yapan kullanıcının UserType claim'inden SystemAdmin (1) olup olmadığını kontrol eder
        /// </summary>
        private bool IsSystemAdmin()
            => User.FindFirst("UserType")?.Value == "1";

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

        // ──────────────────────────────────────────────
        // Sadece SystemAdmin
        // ──────────────────────────────────────────────

        /// <summary>
        /// Tüm kullanıcıları listeler — Sadece SystemAdmin
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] int? clientId = null,
            [FromQuery] int? userType = null)
        {
            if (!IsSystemAdmin())
                return Forbid();

            var query = new GetAllUsersQuery { ClientId = clientId, UserType = userType };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Belirli kullanıcının rolünü günceller — Sadece SystemAdmin
        /// </summary>
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleRequest body)
        {
            if (!IsSystemAdmin())
                return Forbid();

            var command = new UpdateUserRoleCommand { UserId = id, UserType = body.UserType };
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }

    public record UpdateUserRoleRequest(int UserType);
}
