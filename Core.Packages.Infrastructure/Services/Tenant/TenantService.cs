using MagicCarRepairAISupported.Application.Common.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Infrastructure.Services.Tenant
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int? _currentClientId;
        private string _currentLanguage;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetCurrentClientId()
        {
            if (_currentClientId.HasValue)
                return _currentClientId;

            var clientIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("ClientId");
            if (clientIdClaim != null && int.TryParse(clientIdClaim.Value, out var clientId))
            {
                _currentClientId = clientId;
                return clientId;
            }

            // Try to get from headers
            var clientIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Client-Id"].FirstOrDefault();
            if (!string.IsNullOrEmpty(clientIdHeader) && int.TryParse(clientIdHeader, out var headerClientId))
            {
                _currentClientId = headerClientId;
                return headerClientId;
            }

            return null;
        }

        public void SetCurrentClientId(int clientId)
        {
            _currentClientId = clientId;
        }

        public string GetCurrentLanguage()
        {
            if (!string.IsNullOrEmpty(_currentLanguage))
                return _currentLanguage;

            // 1. Try to get from user claims (JWT token - highest priority)
            var languageClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Language");
            if (languageClaim != null && !string.IsNullOrEmpty(languageClaim.Value))
            {
                _currentLanguage = languageClaim.Value;
                return _currentLanguage;
            }

            // 2. Try to get from headers (Accept-Language)
            var languageHeader = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault();
            if (!string.IsNullOrEmpty(languageHeader))
            {
                // Parse Accept-Language header (e.g., "en-US,en;q=0.9,tr;q=0.8")
                var language = languageHeader.Split(',').FirstOrDefault()?.Split('-').FirstOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(language))
                {
                    _currentLanguage = language;
                    return language;
                }
            }

            // 3. Try to get from cookie
            if (_httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue("language", out var cookieLang) == true && !string.IsNullOrEmpty(cookieLang))
            {
                _currentLanguage = cookieLang;
                return cookieLang;
            }

            // 4. Default to Turkish
            _currentLanguage = "tr";
            return _currentLanguage;
        }

        public void SetCurrentLanguage(string language)
        {
            _currentLanguage = language;
        }
    }
}

