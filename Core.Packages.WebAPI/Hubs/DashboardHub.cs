using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace MagicCarRepairAISupported.WebAPI.Hubs
{
    /// <summary>
    /// Dashboard real-time güncellemeleri hub'ı
    /// </summary>
    [Authorize]
    public class DashboardHub : Hub
    {
        /// <summary>
        /// Kullanıcı bağlandığında kendi grubuna ve client grubuna eklenir
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            var clientId = GetClientId();

            if (userId.HasValue)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId.Value}");
            }

            if (clientId.HasValue)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Client_{clientId.Value}");
            }

            await Clients.Caller.SendAsync("Connected", new { UserId = userId, ClientId = clientId });
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Kullanıcı bağlantısı kesildiğinde gruptan çıkarılır
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();
            var clientId = GetClientId();

            if (userId.HasValue)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{userId.Value}");
            }

            if (clientId.HasValue)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Client_{clientId.Value}");
            }

            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Dashboard güncellemelerini dinlemek için client grubuna katıl
        /// </summary>
        public async Task JoinDashboardGroup(int clientId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Client_{clientId}");
            await Clients.Caller.SendAsync("JoinedDashboardGroup", clientId);
        }

        /// <summary>
        /// Dashboard grubundan çık
        /// </summary>
        public async Task LeaveDashboardGroup(int clientId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Client_{clientId}");
            await Clients.Caller.SendAsync("LeftDashboardGroup", clientId);
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

        /// <summary>
        /// HttpContext'ten Client ID'yi al
        /// </summary>
        private int? GetClientId()
        {
            var clientIdClaim = Context.User?.FindFirst("ClientId")?.Value;
            if (int.TryParse(clientIdClaim, out var clientId))
            {
                return clientId;
            }
            return null;
        }
    }
}
