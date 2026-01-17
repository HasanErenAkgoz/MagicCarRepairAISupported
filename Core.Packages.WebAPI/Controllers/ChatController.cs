using MagicCarRepairAISupported.Application.Features.Chat.Commands.MarkAsRead;
using MagicCarRepairAISupported.Application.Features.Chat.Commands.SendMessage;
using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetConversation;
using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetUnreadCount;
using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetWorkOrderMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Mesaj gönder
        /// </summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendChatMessageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İki kullanıcı arasındaki konuşmayı getir
        /// </summary>
        [HttpGet("conversation/{otherUserId}")]
        public async Task<IActionResult> GetConversation(int otherUserId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            var query = new GetConversationQuery
            {
                OtherUserId = otherUserId,
                Skip = skip,
                Take = take
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// WorkOrder'a ait mesajları getir
        /// </summary>
        [HttpGet("workorder/{workOrderId}")]
        public async Task<IActionResult> GetWorkOrderMessages(int workOrderId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            var query = new GetWorkOrderMessagesQuery
            {
                WorkOrderId = workOrderId,
                Skip = skip,
                Take = take
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Okunmamış mesaj sayısını getir
        /// </summary>
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var query = new GetUnreadMessageCountQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Mesajları okundu olarak işaretle
        /// </summary>
        [HttpPost("mark-as-read")]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkMessagesAsReadCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
