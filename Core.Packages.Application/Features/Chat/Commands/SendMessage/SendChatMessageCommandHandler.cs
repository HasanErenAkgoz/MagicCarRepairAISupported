using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Common.Services.WorkOrders;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.Chat.Commands.SendMessage
{
    public class SendChatMessageCommandHandler : IRequestHandler<SendChatMessageCommand, SendChatMessageResponse>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ITenantService _tenantService;
        private readonly IUserRepository _userRepository;
        private readonly ISignalRNotificationService _signalRService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkOrderParticipantAuthorizationService _participantAuthorization;

        public SendChatMessageCommandHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService,
            IUserRepository userRepository,
            ISignalRNotificationService signalRService,
            IHttpContextAccessor httpContextAccessor,
            IWorkOrderParticipantAuthorizationService participantAuthorization)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
            _userRepository = userRepository;
            _signalRService = signalRService;
            _httpContextAccessor = httpContextAccessor;
            _participantAuthorization = participantAuthorization;
        }

        public async Task<SendChatMessageResponse> Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");
            
            // User ID'yi HttpContext'ten al
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var senderId))
            {
                throw new DomainException("USER_ID_REQUIRED");
            }

            if (!request.WorkOrderId.HasValue)
                throw new DomainException("WORK_ORDER_CHAT_REQUIRED");
            if (request.MessageType != Domain.Enums.ChatMessageType.Text || request.FilePath is not null || request.FileName is not null || request.FileSize is not null)
                throw new DomainException("CHAT_ATTACHMENTS_NOT_SUPPORTED");
            if (request.AttachmentIds.Count > 5 || request.AttachmentIds.Any(id => id <= 0) || request.AttachmentIds.Distinct().Count() != request.AttachmentIds.Count)
                throw new DomainException("INVALID_CHAT_ATTACHMENT");
            if (string.IsNullOrWhiteSpace(request.Message) && request.AttachmentIds.Count == 0)
                throw new DomainException("CHAT_MESSAGE_REQUIRED");
            await _participantAuthorization.EnsureCanAccessChatAsync(request.WorkOrderId.Value, cancellationToken);
            await _participantAuthorization.EnsureUserCanAccessChatAsync(request.WorkOrderId.Value, request.ReceiverId, cancellationToken);

            // Alıcıyı kontrol et (receiver kontrolü opsiyonel - mesaj göndermek için yeterli)
            var receiver = await _userRepository.GetByIdAsync(request.ReceiverId);
            if (receiver == null)
            {
                throw new DomainException("USER_NOT_FOUND", new { ReceiverId = request.ReceiverId });
            }

            // Mesajı oluştur
            var chatMessage = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = request.ReceiverId,
                Message = request.Message,
                MessageType = request.MessageType,
                WorkOrderId = request.WorkOrderId,
                // The work order is the source of truth; never accept a caller supplied customer link.
                CustomerId = null,
                FilePath = null,
                FileName = null,
                FileSize = null,
                SentDate = DateTime.UtcNow,
                ClientId = clientId
            };

            // The repository verifies ownership, tenant, work order, expiry and
            // unbound state in the same transaction that creates the message.
            await _chatMessageRepository.CreateWithAttachmentsAsync(
                chatMessage,
                request.AttachmentIds,
                senderId,
                DateTime.UtcNow,
                cancellationToken);

            // Real-time bildirim gönder
            await _signalRService.SendNotificationToUserAsync(
                request.ReceiverId,
                "Yeni Mesaj",
                request.Message,
                "Chat",
                chatMessage.Id);

            return new SendChatMessageResponse
            {
                MessageId = chatMessage.Id,
                SentDate = chatMessage.SentDate
            };
        }
    }
}
