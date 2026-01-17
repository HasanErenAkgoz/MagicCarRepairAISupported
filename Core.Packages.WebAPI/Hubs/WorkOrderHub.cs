using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MagicCarRepairAISupported.WebAPI.Hubs
{
    /// <summary>
    /// WorkOrder real-time güncellemeleri hub'ı
    /// </summary>
    [Authorize]
    public class WorkOrderHub : Hub
    {
        /// <summary>
        /// Kullanıcı bağlandığında kendi grubuna eklenir
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            if (userId.HasValue)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId.Value}");
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
        /// Belirli bir WorkOrder'ı dinlemek için gruba katıl
        /// </summary>
        public async Task JoinWorkOrderGroup(int workOrderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"WorkOrder_{workOrderId}");
        }

        /// <summary>
        /// WorkOrder grubundan çık
        /// </summary>
        public async Task LeaveWorkOrderGroup(int workOrderId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"WorkOrder_{workOrderId}");
        }

        /// <summary>
        /// Client ID'ye göre gruba katıl
        /// </summary>
        public async Task JoinClientGroup(int clientId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Client_{clientId}");
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

