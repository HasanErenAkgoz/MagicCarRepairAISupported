using MagicCarRepairAISupported.Application.Features.Notifications.Commands.MarkAsRead;
using MagicCarRepairAISupported.Application.Features.Notifications.Commands.Send;
using MagicCarRepairAISupported.Application.Features.Notifications.Commands.SendPushNotification;
using MagicCarRepairAISupported.Application.Features.Notifications.Queries.GetByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Bildirim gönderir
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SendNotificationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Kullanıcının bildirimlerini getirir
        /// </summary>
        [HttpGet("my-notifications")]
        public async Task<IActionResult> GetMyNotifications([FromQuery] GetNotificationsByUserQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Okunmamış bildirimleri getirir
        /// </summary>
        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadNotifications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var query = new GetNotificationsByUserQuery
            {
                UnreadOnly = true,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Bildirimi okundu olarak işaretler
        /// </summary>
        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var command = new MarkNotificationAsReadCommand { NotificationId = id };
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Push notification gönderir
        /// </summary>
        [HttpPost("push")]
        public async Task<IActionResult> SendPushNotification([FromBody] SendPushNotificationCommand command)
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

