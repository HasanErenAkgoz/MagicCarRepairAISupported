using MagicCarRepairAISupported.Application.Features.Notifications.Commands.SendWhatsAppMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class WhatsAppController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WhatsAppController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// WhatsApp mesajı gönderir
        /// </summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendWhatsAppMessageCommand command)
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
