namespace MagicCarRepairAISupported.Application.Common.Services
{
    /// <summary>
    /// Service to get localized error messages
    /// </summary>
    public interface IErrorMessageService
    {
        /// <summary>
        /// Get localized message by error code
        /// </summary>
        Task<string> GetMessageAsync(string errorCode, string? language = null, object? parameters = null);
        
        /// <summary>
        /// Get localized message by error code (synchronous)
        /// </summary>
        string GetMessage(string errorCode, string? language = null, object? parameters = null);
        
        /// <summary>
        /// Check if error code exists
        /// </summary>
        Task<bool> ExistsAsync(string errorCode, string? language = null);
        
        /// <summary>
        /// Reload all messages from database into cache
        /// </summary>
        Task ReloadMessagesAsync();
    }
}

