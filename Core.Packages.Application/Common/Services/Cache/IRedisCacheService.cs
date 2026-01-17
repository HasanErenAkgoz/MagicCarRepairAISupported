namespace MagicCarRepairAISupported.Application.Common.Services.Cache
{
    /// <summary>
    /// Redis cache service interface
    /// </summary>
    public interface IRedisCacheService
    {
        /// <summary>
        /// Set cache value
        /// </summary>
        Task SetCacheValueAsync(string key, string value);

        /// <summary>
        /// Get cache value
        /// </summary>
        Task<string?> GetCacheValueAsync(string key);

        /// <summary>
        /// Remove cache by key
        /// </summary>
        Task RemoveAsync(string key);

        /// <summary>
        /// Remove cache by pattern (e.g., "parts:*")
        /// </summary>
        Task RemoveByPatternAsync(string pattern);
    }
}
