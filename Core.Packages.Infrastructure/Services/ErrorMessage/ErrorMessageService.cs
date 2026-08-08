using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace MagicCarRepairAISupported.Infrastructure.Services.ErrorMessage
{
    public class ErrorMessageService : IErrorMessageService
    {
        private readonly IErrorMessageRepository _errorMessageRepository;
        private readonly ITenantService _tenantService;
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY_PREFIX = "ErrorMessages_";
        private const int CACHE_DURATION_MINUTES = 60;

        public ErrorMessageService(
            IErrorMessageRepository errorMessageRepository, 
            ITenantService tenantService,
            IMemoryCache cache)
        {
            _errorMessageRepository = errorMessageRepository;
            _tenantService = tenantService;
            _cache = cache;
        }

        public async Task<string> GetMessageAsync(string errorCode, string? language = null, object? parameters = null)
        {
            language ??= _tenantService.GetCurrentLanguage();

            // Normalize cases where callers pass exception message text instead of pure error code.
            // Example: DomainException base message is "Translation key: SOME_CODE"
            const string translationKeyPrefix = "Translation key: ";
            if (!string.IsNullOrWhiteSpace(errorCode) &&
                errorCode.StartsWith(translationKeyPrefix, StringComparison.Ordinal))
            {
                errorCode = errorCode[translationKeyPrefix.Length..].Trim();
            }
            
            var cacheKey = $"{CACHE_KEY_PREFIX}{language}_{errorCode}";
            
            // Try to get from cache
            if (_cache.TryGetValue(cacheKey, out string? cachedMessage) && cachedMessage != null)
            {
                return FormatMessage(cachedMessage, parameters);
            }

            // Get from database
            var errorMessage = await _errorMessageRepository.GetByCodeAndLanguageAsync(errorCode, language);
            
            if (errorMessage != null)
            {
                // Cache the message
                _cache.Set(cacheKey, errorMessage.Message, TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));
                return FormatMessage(errorMessage.Message, parameters);
            }

            // Fallback: Try to get in default language (Turkish)
            if (language != "tr")
            {
                var defaultMessage = await _errorMessageRepository.GetByCodeAndLanguageAsync(errorCode, "tr");
                if (defaultMessage != null)
                {
                    return FormatMessage(defaultMessage.Message, parameters);
                }
            }

            // If not found, return the error code itself
            return errorCode;
        }

        public string GetMessage(string errorCode, string? language = null, object? parameters = null)
        {
            // For synchronous calls, we can't await, so we use GetAwaiter().GetResult()
            // This is not ideal but necessary for compatibility with static Messages class
            return GetMessageAsync(errorCode, language, parameters).GetAwaiter().GetResult();
        }

        public async Task<bool> ExistsAsync(string errorCode, string? language = null)
        {
            language ??= _tenantService.GetCurrentLanguage();
            var errorMessage = await _errorMessageRepository.GetByCodeAndLanguageAsync(errorCode, language);
            return errorMessage != null;
        }

        public async Task ReloadMessagesAsync()
        {
            // Clear all cached messages
            // Note: This is a simple implementation. For production, consider using a more sophisticated cache invalidation strategy
            var languages = new[] { "tr", "en", "ar", "de", "fr" }; // Add your supported languages
            
            foreach (var language in languages)
            {
                var messages = await _errorMessageRepository.GetByLanguageAsync(language);
                foreach (var message in messages)
                {
                    var cacheKey = $"{CACHE_KEY_PREFIX}{language}_{message.ErrorCode}";
                    _cache.Set(cacheKey, message.Message, TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));
                }
            }
        }

        private string FormatMessage(string message, object? parameters)
        {
            if (parameters == null)
                return message;

            // Replace placeholders like {PropertyName}, {Value}, etc.
            var properties = parameters.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var placeholder = $"{{{prop.Name}}}";
                var value = prop.GetValue(parameters)?.ToString() ?? string.Empty;
                message = message.Replace(placeholder, value);
            }

            return message;
        }
    }
}

