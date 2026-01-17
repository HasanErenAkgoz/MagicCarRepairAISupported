using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Infrastructure.Services.Notification;
using MagicCarRepairAISupported.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace MagicCarRepairAISupported.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            // SignalR Hub Context'lerini register et
            services.AddScoped<ISignalRNotificationService>(sp =>
            {
                var notificationHub = (IHubContext)sp.GetRequiredService<IHubContext<NotificationHub>>();
                var workOrderHub = (IHubContext)sp.GetRequiredService<IHubContext<WorkOrderHub>>();
                var chatHub = (IHubContext)sp.GetRequiredService<IHubContext<ChatHub>>();
                var dashboardHub = (IHubContext)sp.GetRequiredService<IHubContext<DashboardHub>>();
                var logger = sp.GetRequiredService<ILogger<SignalRNotificationService>>();
                
                return new SignalRNotificationService(notificationHub, workOrderHub, chatHub, dashboardHub, logger);
            });

            return services;
        }
    }
}

