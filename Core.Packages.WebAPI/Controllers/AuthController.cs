using MagicCarRepairAISupported.Application.Features.Auth.Commands.ImpersonateUser;
using MagicCarRepairAISupported.Application.Features.Auth.Commands.ImpersonateClient;
using MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands;
using MagicCarRepairAISupported.Application.Features.Auth.Login.Commands;
using MagicCarRepairAISupported.Application.Features.Auth.Register.Commands;
using MagicCarRepairAISupported.Application.Features.Auth.ResetPassword;
using MagicCarRepairAISupported.Application.Features.Auth.ChangePassword;
using MagicCarRepairAISupported.Application.Features.Auth.Commands.Complete2FALogin;
using MagicCarRepairAISupported.Application.Features.Auth.Commands.Enable2FA;
using MagicCarRepairAISupported.Application.Features.Auth.Commands.Disable2FA;
using MagicCarRepairAISupported.Application.Features.Auth.RefreshToken.Commands;
using MagicCarRepairAISupported.Application.Features.Email.SendEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using WebAPI.Controllers;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var result = await Mediator.Send(loginCommand);
            
            // 2FA gerekiyorsa özel response döndür
            if (!result.Success && result.Message == "TwoFactorRequired" && result.Data?.User?.RequiresTwoFactor == true)
            {
                return Ok(new
                {
                    success = false,
                    requiresTwoFactor = true,
                    user = result.Data.User,
                    message = "2FA doğrulaması gerekiyor"
                });
            }
            
            return GetResponse(result);
        }

        /// <summary>
        /// 2FA doğrulandıktan sonra login'i tamamlar
        /// </summary>
        [HttpPost("complete-2fa-login")]
        [AllowAnonymous]
        public async Task<IActionResult> Complete2FALogin([FromBody] Complete2FALoginCommand command)
        {
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var result = await Mediator.Send(registerCommand);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("sendemail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand sendEmailCommand)
        {
            var result = await Mediator.Send(sendEmailCommand);
            return result.Success ? Ok(result) : BadRequest(result);

        }

        /// <summary>
        /// Şifre sıfırlama için OTP gönderir
        /// </summary>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        /// <summary>
        /// OTP kodunu doğrular ve reset token döner
        /// </summary>
        [HttpPost("verify-reset-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyResetOtp([FromBody] VerifyResetOtpCommand command)
        {
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        /// <summary>
        /// Reset token ile şifreyi sıfırlar
        /// </summary>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// OTP kodunu yeniden gönderir
        /// </summary>
        [HttpPost("resend-reset-otp")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendResetOtp([FromBody] ResendResetOtpCommand command)
        {
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        [HttpPost("register-shop")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterShop([FromBody] RegisterShopCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("register-customer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcının şifresini değiştirir
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 2FA'yı aktif eder
        /// </summary>
        [HttpPost("enable-2fa")]
        [Authorize]
        public async Task<IActionResult> Enable2FA()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { success = false, message = "Unauthorized. Please log in again." });
            }

            var command = new Enable2FACommand { UserId = userId };
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        /// <summary>
        /// 2FA'yı devre dışı bırakır
        /// </summary>
        [HttpPost("disable-2fa")]
        [Authorize]
        public async Task<IActionResult> Disable2FA([FromBody] Disable2FARequest? request = null)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { success = false, message = "Unauthorized. Please log in again." });
            }

            var command = new Disable2FACommand
            {
                UserId = userId,
                Password = request?.Password
            };
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        /// <summary>
        /// SystemAdmin obtains a token as another user (impersonation).
        /// Only SystemAdmin (UserType=1) may call.
        /// </summary>
        [HttpPost("impersonate")]
        [Authorize]
        public async Task<IActionResult> ImpersonateUser([FromBody] ImpersonateUserCommand command)
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value;
            if (userTypeClaim != "1")
                return Forbid();

            var result = await Mediator.Send(command);
            return GetResponse(result);
        }
    
        /// <summary>
        /// SystemAdmin'in shop kullanıcısı olmayan bir tenant için otomatik teknik Manager hesabıyla token almasını sağlar.
        /// Sadece SystemAdmin (UserType=1) çağırabilir.
        /// </summary>
        [HttpPost("impersonate-client")]
        [Authorize]
        public async Task<IActionResult> ImpersonateClient([FromBody] ImpersonateClientCommand command)
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value;
            if (userTypeClaim != "1")
                return Forbid();

            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] MagicCarRepairAISupported.Application.Features.Auth.RefreshToken.Commands.RefreshTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }
    }
}
