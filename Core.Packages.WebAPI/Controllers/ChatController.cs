using MagicCarRepairAISupported.Application.Features.Chat.Commands.MarkAsRead;
using MagicCarRepairAISupported.Application.Features.Chat.Commands.SendMessage;
using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetConversation;
using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetUnreadCount;
using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetWorkOrderMessages;
using MediatR;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Common.Services.WorkOrders;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly BaseDbContext _db;
        private readonly IPrivateMediaStorage _storage;
        private readonly IWorkOrderParticipantAuthorizationService _participants;
        private readonly ITenantService _tenant;

        public ChatController(IMediator mediator, BaseDbContext db, IPrivateMediaStorage storage, IWorkOrderParticipantAuthorizationService participants, ITenantService tenant)
        {
            _mediator = mediator;
            _db = db;
            _storage = storage;
            _participants = participants;
            _tenant = tenant;
        }

        [HttpPost("workorder/{workOrderId}/attachments")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAttachment(int workOrderId, IFormFile file, CancellationToken ct)
        {
            await _participants.EnsureCanAccessChatAsync(workOrderId, ct);
            if (file is null || file.Length is <= 0 or > 10 * 1024 * 1024 || file.ContentType is not ("image/jpeg" or "image/png" or "image/webp" or "application/pdf"))
                throw new DomainException("INVALID_CHAT_ATTACHMENT");

            var userId = UserId();
            var clientId = _tenant.GetRequiredClientId();
            var key = await _storage.StoreAsync(file, $"chat-drafts/{clientId}/{workOrderId}/{userId}", ct);
            try
            {
                var item = new ChatAttachment
                {
                    ClientId = clientId, WorkOrderId = workOrderId, OwnerUserId = userId,
                    StorageKey = key, OriginalFileName = Path.GetFileName(file.FileName), ContentType = file.ContentType,
                    Length = file.Length, ExpiresAt = DateTime.UtcNow.AddHours(1), CreatedDate = DateTime.UtcNow, CreatedBy = userId
                };
                _db.ChatAttachments.Add(item);
                await _db.SaveChangesAsync(ct);
                return Ok(new { attachmentId = item.Id, expiresAt = item.ExpiresAt, fileName = item.OriginalFileName, contentType = item.ContentType, length = item.Length });
            }
            catch
            {
                await _storage.DeleteAsync(key, ct);
                throw;
            }
        }

        [HttpGet("workorder/{workOrderId}/attachments/{attachmentId}/download")]
        public async Task<IActionResult> DownloadAttachment(int workOrderId, int attachmentId, CancellationToken ct)
        {
            await _participants.EnsureCanAccessChatAsync(workOrderId, ct);
            var clientId = _tenant.GetRequiredClientId();
            var item = await _db.ChatAttachments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == attachmentId && x.WorkOrderId == workOrderId && x.ClientId == clientId && x.ChatMessageId != null, ct)
                ?? throw new DomainException("CHAT_ATTACHMENT_NOT_FOUND");
            if (!PrivateMediaStorageKey.IsValid(item.StorageKey))
                throw new DomainException("CHAT_ATTACHMENT_NOT_FOUND");
            var stream = await _storage.OpenReadAsync(item.StorageKey, ct) ?? throw new DomainException("CHAT_ATTACHMENT_NOT_FOUND");
            return File(stream, item.ContentType, item.OriginalFileName, enableRangeProcessing: true);
        }

        private int UserId() => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : throw new DomainException("USER_ID_REQUIRED");

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
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
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

        [HttpPost("workorder/{workOrderId}/participants")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> AddParticipant(int workOrderId, [FromBody] MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddParticipant.AddWorkOrderParticipantCommand command)
        {
            command.WorkOrderId = workOrderId;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("workorder/{workOrderId}/participants/{userId}")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> RemoveParticipant(int workOrderId, int userId)
            => Ok(await _mediator.Send(new MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveParticipant.RemoveWorkOrderParticipantCommand { WorkOrderId = workOrderId, UserId = userId }));

        /// <summary>
        /// Okunmamış mesaj sayısını getir
        /// </summary>
        [HttpGet("unread-count")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
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
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> MarkAsRead([FromBody] MarkMessagesAsReadCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
