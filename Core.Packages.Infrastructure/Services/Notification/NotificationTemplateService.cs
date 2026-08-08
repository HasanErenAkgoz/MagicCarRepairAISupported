using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.Notification
{
    public class NotificationTemplateService : INotificationTemplateService
    {
        private readonly IEntityRepository<NotificationTemplate, int> _templateRepository;
        private readonly INotificationService _notificationService;
        private readonly ITenantService _tenantService;
        private readonly ILogger<NotificationTemplateService> _logger;

        public NotificationTemplateService(
            IEntityRepository<NotificationTemplate, int> templateRepository,
            INotificationService notificationService,
            ITenantService tenantService,
            ILogger<NotificationTemplateService> logger)
        {
            _templateRepository = templateRepository;
            _notificationService = notificationService;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<bool> SendNotificationByTemplateAsync(
            string templateName,
            int? userId,
            string? email,
            string? phoneNumber,
            Dictionary<string, object> variables,
            string? relatedEntityType = null,
            int? relatedEntityId = null)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                // Şablonu bul
                var template = await _templateRepository.Query()
                    .FirstOrDefaultAsync(t => t.Name == templateName && 
                                             t.ClientId == clientId && 
                                             t.IsActive);

                if (template == null)
                {
                    _logger.LogWarning($"Notification template '{templateName}' not found for client {clientId}");
                    return false;
                }

                // Şablonu doldur
                var title = template.GetFilledTitle(variables);
                var content = template.GetFilledContent(variables);

                // Bildirimi gönder
                return await _notificationService.SendNotificationAsync(
                    template.Type,
                    userId,
                    email,
                    phoneNumber,
                    title,
                    content,
                    relatedEntityType ?? template.RelatedEntityType,
                    relatedEntityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending notification by template '{templateName}'");
                return false;
            }
        }
    }
}

