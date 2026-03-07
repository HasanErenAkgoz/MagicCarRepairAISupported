using MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands;
using MagicCarRepairAISupported.Application.Features.Auth.Login.Commands;
using MagicCarRepairAISupported.Application.Features.Auth.Register.Commands;
using MagicCarRepairAISupported.Application.Features.Auth.ResetPassword;
using MagicCarRepairAISupported.Application.Features.Auth.ChangePassword;
using MagicCarRepairAISupported.Application.Features.Auth.Commands.Complete2FALogin;
using MagicCarRepairAISupported.Application.Features.Email.SendEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        [HttpPost("login")]
        [AllowAnonymous]
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

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);

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
    }
}
