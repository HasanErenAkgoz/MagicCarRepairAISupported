using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Audit;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.Audit
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditLogService(
            IAuditLogRepository auditLogRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _auditLogRepository = auditLogRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(
            string entityName,
            int entityId,
            string action,
            string? oldValues = null,
            string? newValues = null,
            string? changedProperties = null,
            string? description = null,
            bool isSuccess = true,
            string? errorMessage = null,
            long? durationMs = null,
            CancellationToken cancellationToken = default)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString();
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();
            var requestPath = httpContext?.Request?.Path.Value;
            var requestMethod = httpContext?.Request?.Method;

            var auditLog = new AuditLog
            {
                UserId = userId != null ? int.Parse(userId) : null,
                EntityName = entityName,
                EntityId = entityId,
                Action = action,
                OldValues = oldValues,
                NewValues = newValues,
                ChangedProperties = changedProperties,
                Description = description,
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage,
                DurationMs = durationMs,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                RequestPath = requestPath,
                RequestMethod = requestMethod,
                ClientId = clientId
            };

            await _auditLogRepository.AddAsync(auditLog, cancellationToken);
            await _auditLogRepository.SaveChangesAsync();
        }
    }
}
