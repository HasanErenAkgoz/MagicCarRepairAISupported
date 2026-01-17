using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Common.Services.Translation
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CACHE_KEY_PREFIX = "translation_";
        private const int CACHE_DURATION_MINUTES = 60;

        public TranslationService(
            ITranslationRepository translationRepository,
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _translationRepository = translationRepository;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTranslationAsync(string key, string language = "tr", string? defaultValue = null)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue ?? key;

            var cacheKey = $"{CACHE_KEY_PREFIX}{language}_{key}";
            
            if (_cache.TryGetValue(cacheKey, out string? cachedValue))
                return cachedValue ?? defaultValue ?? key;

            var translation = await _translationRepository
                .GetAsync(t => t.Key == key && t.Language == language, CancellationToken.None);

            var result = translation?.Value ?? defaultValue ?? key;
            
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));
            
            return result;
        }

        public async Task<string> GetTranslationAsync(string key, string? defaultValue = null)
        {
            var language = GetCurrentLanguage();
            return await GetTranslationAsync(key, language, defaultValue);
        }

        public async Task<string> GetDomainExceptionMessageAsync(string errorCode, object? parameters = null, string language = "tr")
        {
            var translationKey = $"DomainException.{errorCode}";
            var message = await GetTranslationAsync(translationKey, language, errorCode);

            if (parameters != null)
            {
                message = FormatMessage(message, parameters);
            }

            return message;
        }

        public async Task<List<string>> GetSupportedLanguagesAsync()
        {
            var cacheKey = $"{CACHE_KEY_PREFIX}supported_languages";
            
            if (_cache.TryGetValue(cacheKey, out List<string>? cachedLanguages))
                return cachedLanguages ?? new List<string> { "tr" };

            var languages = await _translationRepository
                .GetListAsync(CancellationToken.None, t => t.Status == Domain.Enums.Status.Active);

            var uniqueLanguages = languages
                .Select(t => t.Language)
                .Distinct()
                .OrderBy(l => l)
                .ToList();

            if (!uniqueLanguages.Any())
                uniqueLanguages = new List<string> { "tr" };

            _cache.Set(cacheKey, uniqueLanguages, TimeSpan.FromHours(1));
            
            return uniqueLanguages;
        }

        public async Task<Dictionary<string, string>> GetAllTranslationsAsync(string language = "tr")
        {
            var cacheKey = $"{CACHE_KEY_PREFIX}all_{language}";
            
            if (_cache.TryGetValue(cacheKey, out Dictionary<string, string>? cachedTranslations))
                return cachedTranslations ?? new Dictionary<string, string>();

            var translations = await _translationRepository
                .GetListAsync(CancellationToken.None, t => t.Language == language && t.Status == Domain.Enums.Status.Active);

            var result = translations.ToDictionary(t => t.Key, t => t.Value);
            
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(CACHE_DURATION_MINUTES));
            
            return result;
        }

        private string GetCurrentLanguage()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return "tr";

            // 1. Query string'den dil al (api/endpoint?lang=en)
            if (httpContext.Request.Query.TryGetValue("lang", out var queryLang))
                return queryLang.ToString();

            // 2. Header'dan dil al (Accept-Language: en-US,en;q=0.9)
            if (httpContext.Request.Headers.TryGetValue("Accept-Language", out var acceptLanguage))
            {
                var languages = acceptLanguage.ToString().Split(',')
                    .Select(lang => lang.Split(';')[0].Trim().Split('-')[0])
                    .Where(lang => !string.IsNullOrEmpty(lang))
                    .ToList();

                if (languages.Any())
                    return languages.First();
            }

            // 3. Cookie'den dil al
            if (httpContext.Request.Cookies.TryGetValue("language", out var cookieLang))
                return cookieLang;

            // 4. Varsayılan dil
            return "tr";
        }

        private string FormatMessage(string message, object parameters)
        {
            try
            {
                if (parameters is Dictionary<string, object> dict)
                {
                    foreach (var kvp in dict)
                    {
                        message = message.Replace($"{{{kvp.Key}}}", kvp.Value?.ToString() ?? "");
                    }
                }
                else
                {
                    // Reflection ile object properties'lerini al
                    var properties = parameters.GetType().GetProperties();
                    foreach (var prop in properties)
                    {
                        var value = prop.GetValue(parameters)?.ToString() ?? "";
                        message = message.Replace($"{{{prop.Name}}}", value);
                    }
                }
            }
            catch
            {
                // Format hatası durumunda orijinal mesajı döndür
            }

            return message;
        }
    }
}
