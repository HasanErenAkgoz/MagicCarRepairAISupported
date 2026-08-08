using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Common.Services
{
    /// <summary>
    /// Service to manage current tenant/client context
    /// </summary>
    public interface ITenantService
    {
        int? GetCurrentClientId();

        /// <summary>
        /// Requires ClientId from JWT (or SA header override). Throws when missing.
        /// </summary>
        int GetRequiredClientId();

        /// <summary>
        /// Anonymous registration/bootstrap only — never use for authenticated shop APIs.
        /// </summary>
        int GetClientIdOrDefault(int defaultClientId = 1);

        UserType? GetCurrentUserType();

        void SetCurrentClientId(int clientId);
        string GetCurrentLanguage();
        void SetCurrentLanguage(string language);
    }
}

