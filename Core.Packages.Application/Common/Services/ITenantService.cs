namespace MagicCarRepairAISupported.Application.Common.Services
{
    /// <summary>
    /// Service to manage current tenant/client context
    /// </summary>
    public interface ITenantService
    {
        int? GetCurrentClientId();
        void SetCurrentClientId(int clientId);
        string GetCurrentLanguage();
        void SetCurrentLanguage(string language);
    }
}

