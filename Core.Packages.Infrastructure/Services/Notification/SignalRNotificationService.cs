using MagicCarRepairAISupported.Application.Common.Services.Notification;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.Notification
{
    public class SignalRNotificationService : ISignalRNotificationService
    {
        private readonly IHubContext _notificationHub;
        private readonly IHubContext _workOrderHub;
        private readonly IHubContext? _chatHub;
        private readonly IHubContext? _dashboardHub;
        private readonly ILogger<SignalRNotificationService> _logger;

        public SignalRNotificationService(
            IHubContext notificationHub,
            IHubContext workOrderHub,
            IHubContext? chatHub = null,
            IHubContext? dashboardHub = null,
            ILogger<SignalRNotificationService> logger = null!)
        {
            _notificationHub = notificationHub;
            _workOrderHub = workOrderHub;
            _chatHub = chatHub;
            _dashboardHub = dashboardHub;
            _logger = logger;
        }

        public async Task SendNotificationToUserAsync(int userId, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null)
        {
            try
            {
                await _notificationHub.Clients.Group($"User_{userId}").SendAsync("ReceiveNotification", new
                {
                    Title = title,
                    Content = content,
                    RelatedEntityType = relatedEntityType,
                    RelatedEntityId = relatedEntityId,
                    Timestamp = DateTime.UtcNow
                });

                _logger.LogInformation($"Real-time notification sent to user {userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending real-time notification to user {userId}");
            }
        }

        public async Task SendNotificationToClientAsync(int clientId, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null)
        {
            try
            {
                await _notificationHub.Clients.Group($"Client_{clientId}").SendAsync("ReceiveNotification", new
                {
                    Title = title,
                    Content = content,
                    RelatedEntityType = relatedEntityType,
                    RelatedEntityId = relatedEntityId,
                    Timestamp = DateTime.UtcNow
                });

                _logger.LogInformation($"Real-time notification sent to client {clientId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending real-time notification to client {clientId}");
            }
        }

        public async Task SendWorkOrderUpdateAsync(int workOrderId, string status, string message, int? userId = null)
        {
            try
            {
                var updateData = new
                {
                    WorkOrderId = workOrderId,
                    Status = status,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };

                // WorkOrder grubuna gönder
                await _workOrderHub.Clients.Group($"WorkOrder_{workOrderId}").SendAsync("WorkOrderUpdated", updateData);

                // Eğer userId varsa, kullanıcıya da gönder
                if (userId.HasValue)
                {
                    await _workOrderHub.Clients.Group($"User_{userId.Value}").SendAsync("WorkOrderUpdated", updateData);
                }

                // Dashboard güncellemesi gönder (eğer DashboardHub varsa)
                if (_dashboardHub != null)
                {
                    // WorkOrder'dan client ID'yi almak için repository'ye ihtiyaç var
                    // Şimdilik tüm client'lara gönderiyoruz, daha sonra optimize edilebilir
                    await _dashboardHub.Clients.All.SendAsync("WorkOrderUpdated", updateData);
                }

                _logger.LogInformation($"Real-time WorkOrder update sent for WorkOrder {workOrderId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending real-time WorkOrder update for WorkOrder {workOrderId}");
            }
        }
    }
}

