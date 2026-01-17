namespace MagicCarRepairAISupported.Application.Common.Services.Audit
{
    public interface IAuditLogService
    {
        Task LogAsync(
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
            CancellationToken cancellationToken = default);
    }
}
