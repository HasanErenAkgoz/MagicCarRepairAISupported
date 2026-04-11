namespace MagicCarRepairAISupported.Application.Common.Tenant
{
    public static class TenantShadowUser
    {
        public const string EmailDomain = "internal.magiccarrepair.local";

        public static string GetEmail(int clientId) => $"tenant-shadow-{clientId}@{EmailDomain}";

        public static bool IsShadowEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var e = email.Trim();
            return e.EndsWith($"@{EmailDomain}", StringComparison.OrdinalIgnoreCase)
                   && e.StartsWith("tenant-shadow-", StringComparison.OrdinalIgnoreCase);
        }
    }
}
