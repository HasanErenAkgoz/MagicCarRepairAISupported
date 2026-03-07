using MagicCarRepairAISupported.Application.Features.DeviceTokens.Commands.RegisterDeviceToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/device-tokens")]
    [Authorize]
    public class DeviceTokensController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeviceTokensController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cihaz token'ını kaydeder (FCM push notifications için)
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterDeviceToken([FromBody] RegisterDeviceTokenCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
