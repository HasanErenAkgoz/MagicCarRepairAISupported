using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
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

        public SendChatMessageCommandHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService,
            IUserRepository userRepository,
            ISignalRNotificationService signalRService,
            IHttpContextAccessor httpContextAccessor)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
            _userRepository = userRepository;
            _signalRService = signalRService;
            _httpContextAccessor = httpContextAccessor;
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
                CustomerId = request.CustomerId,
                FilePath = request.FilePath,
                FileName = request.FileName,
                FileSize = request.FileSize,
                SentDate = DateTime.UtcNow,
                ClientId = clientId
            };

            await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);
            await _chatMessageRepository.SaveChangesAsync();

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
