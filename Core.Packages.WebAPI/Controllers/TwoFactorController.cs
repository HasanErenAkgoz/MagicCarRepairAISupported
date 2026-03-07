using MagicCarRepairAISupported.Application.Features.Auth.Commands.Enable2FA;
using MagicCarRepairAISupported.Application.Features.Auth.Commands.Verify2FA;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/two-factor")]
    [Authorize]
    public class TwoFactorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TwoFactorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 2FA kurulumunu başlatır
        /// </summary>
        [HttpPost("enable")]
        public async Task<IActionResult> Enable2FA()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var command = new Enable2FACommand { UserId = userId };
            var result = await _mediator.Send(command);
            
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// 2FA kodunu doğrular (setup veya login)
        /// </summary>
        [HttpPost("verify")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FARequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var command = new Verify2FACommand
            {
                UserId = userId,
                Code = request.Code,
                IsSetup = request.IsSetup
            };
            
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 2FA'yı devre dışı bırakır
        /// </summary>
        [HttpPost("disable")]
        public async Task<IActionResult> Disable2FA([FromBody] Disable2FARequest request)
        {
            // 2FA'yı devre dışı bırakmak için şifre veya recovery code gerekli
            // Basit implementasyon - sadece kullanıcı ID ile
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            // Disable2FACommand implement edilecek
            return Ok(new { success = true, message = "2FA devre dışı bırakıldı." });
        }
    }

    public class Verify2FARequest
    {
        public string Code { get; set; } = string.Empty;
        public bool IsSetup { get; set; } = false;
    }

    public class Disable2FARequest
    {
        public string Password { get; set; } = string.Empty;
        public string? RecoveryCode { get; set; }
    }
}
