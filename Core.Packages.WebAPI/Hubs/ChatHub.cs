using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MagicCarRepairAISupported.WebAPI.Hubs
{
    /// <summary>
    /// Real-time chat hub'ı - Müşteri-Servis arası mesajlaşma
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ITenantService _tenantService;

        public ChatHub(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
        }

        /// <summary>
        /// Kullanıcı bağlandığında kendi grubuna eklenir
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId.Value}");
                await Clients.Caller.SendAsync("Connected", $"Connected as user {userId.Value}");
            }

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Kullanıcı bağlantısı kesildiğinde gruptan çıkarılır
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId.Value}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Mesaj gönder
        /// </summary>
        public async Task SendMessage(int receiverId, string message, ChatMessageType messageType = ChatMessageType.Text, int? workOrderId = null, int? customerId = null, string? filePath = null, string? fileName = null, long? fileSize = null, CancellationToken cancellationToken = default)
        {
            var senderId = GetUserId();
            if (!senderId.HasValue)
            {
                await Clients.Caller.SendAsync("Error", "User not authenticated");
                return;
            }

            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Mesajı veritabanına kaydet
            var chatMessage = new ChatMessage
            {
                SenderId = senderId.Value,
                ReceiverId = receiverId,
                Message = message,
                MessageType = messageType,
                WorkOrderId = workOrderId,
                CustomerId = customerId,
                FilePath = filePath,
                FileName = fileName,
                FileSize = fileSize,
                SentDate = DateTime.UtcNow,
                ClientId = clientId
            };

            await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);
            await _chatMessageRepository.SaveChangesAsync();

            // Alıcıya mesaj gönder
            await Clients.Group($"User_{receiverId}").SendAsync("ReceiveMessage", new
            {
                Id = chatMessage.Id,
                SenderId = chatMessage.SenderId,
                ReceiverId = chatMessage.ReceiverId,
                Message = chatMessage.Message,
                MessageType = chatMessage.MessageType,
                WorkOrderId = chatMessage.WorkOrderId,
                CustomerId = chatMessage.CustomerId,
                FilePath = chatMessage.FilePath,
                FileName = chatMessage.FileName,
                FileSize = chatMessage.FileSize,
                SentDate = chatMessage.SentDate,
                IsRead = chatMessage.IsRead
            });

            // Gönderene onay mesajı
            await Clients.Caller.SendAsync("MessageSent", new { MessageId = chatMessage.Id });
        }

        /// <summary>
        /// WorkOrder'a mesaj gönder (grup mesajı)
        /// </summary>
        public async Task SendWorkOrderMessage(int workOrderId, string message, ChatMessageType messageType = ChatMessageType.Text, string? filePath = null, string? fileName = null, long? fileSize = null, CancellationToken cancellationToken = default)
        {
            var senderId = GetUserId();
            if (!senderId.HasValue)
            {
                await Clients.Caller.SendAsync("Error", "User not authenticated");
                return;
            }

            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Mesajı veritabanına kaydet (receiverId null - grup mesajı)
            var chatMessage = new ChatMessage
            {
                SenderId = senderId.Value,
                ReceiverId = null, // Grup mesajı
                Message = message,
                MessageType = messageType,
                WorkOrderId = workOrderId,
                FilePath = filePath,
                FileName = fileName,
                FileSize = fileSize,
                SentDate = DateTime.UtcNow,
                ClientId = clientId
            };

            await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);
            await _chatMessageRepository.SaveChangesAsync();

            // WorkOrder grubundaki herkese mesaj gönder
            await Clients.Group($"WorkOrder_{workOrderId}").SendAsync("ReceiveWorkOrderMessage", new
            {
                Id = chatMessage.Id,
                SenderId = chatMessage.SenderId,
                WorkOrderId = chatMessage.WorkOrderId,
                Message = chatMessage.Message,
                MessageType = chatMessage.MessageType,
                FilePath = chatMessage.FilePath,
                FileName = chatMessage.FileName,
                FileSize = chatMessage.FileSize,
                SentDate = chatMessage.SentDate
            });
        }

        /// <summary>
        /// WorkOrder grubuna katıl
        /// </summary>
        public async Task JoinWorkOrderGroup(int workOrderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"WorkOrder_{workOrderId}");
            await Clients.Caller.SendAsync("JoinedWorkOrderGroup", workOrderId);
        }

        /// <summary>
        /// WorkOrder grubundan çık
        /// </summary>
        public async Task LeaveWorkOrderGroup(int workOrderId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"WorkOrder_{workOrderId}");
            await Clients.Caller.SendAsync("LeftWorkOrderGroup", workOrderId);
        }

        /// <summary>
        /// Mesajları okundu olarak işaretle
        /// </summary>
        public async Task MarkMessagesAsRead(int? senderId = null, int? workOrderId = null)
        {
            var userId = GetUserId();
            if (!userId.HasValue)
            {
                await Clients.Caller.SendAsync("Error", "User not authenticated");
                return;
            }

            await _chatMessageRepository.MarkMessagesAsReadAsync(userId.Value, senderId, workOrderId);
            await Clients.Caller.SendAsync("MessagesMarkedAsRead");
        }

        /// <summary>
        /// HttpContext'ten User ID'yi al
        /// </summary>
        private int? GetUserId()
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return null;
        }
    }
}
