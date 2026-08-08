using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
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

            // X-Client-Id is only honored for SystemAdmin (tenant switching / impersonation)
            if (GetCurrentUserType() == UserType.SystemAdmin)
            {
                var clientIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Client-Id"].FirstOrDefault();
                if (!string.IsNullOrEmpty(clientIdHeader) && int.TryParse(clientIdHeader, out var headerClientId))
                {
                    _currentClientId = headerClientId;
                    return headerClientId;
                }
            }

            return null;
        }

        public int GetRequiredClientId()
        {
            var clientId = GetCurrentClientId();
            if (!clientId.HasValue)
                throw new DomainException("CLIENT_ID_REQUIRED");
            return clientId.Value;
        }

        public int GetClientIdOrDefault(int defaultClientId = 1)
        {
            return GetCurrentClientId() ?? defaultClientId;
        }

        public UserType? GetCurrentUserType()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserType")?.Value;
            if (string.IsNullOrEmpty(claim) || !int.TryParse(claim, out var userTypeInt))
                return null;
            if (!Enum.IsDefined(typeof(UserType), userTypeInt))
                return null;
            return (UserType)userTypeInt;
        }

        public void SetCurrentClientId(int clientId)
        {
            _currentClientId = clientId;
        }

        public string GetCurrentLanguage()
        {
            if (!string.IsNullOrEmpty(_currentLanguage))
                return _currentLanguage;

            var languageClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Language");
            if (languageClaim != null && !string.IsNullOrEmpty(languageClaim.Value))
            {
                _currentLanguage = languageClaim.Value;
                return _currentLanguage;
            }

            var languageHeader = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].FirstOrDefault();
            if (!string.IsNullOrEmpty(languageHeader))
            {
                var language = languageHeader.Split(',').FirstOrDefault()?.Split('-').FirstOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(language))
                {
                    _currentLanguage = language;
                    return language;
                }
            }

            if (_httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue("language", out var cookieLang) == true
                && !string.IsNullOrEmpty(cookieLang))
            {
                _currentLanguage = cookieLang;
                return cookieLang;
            }

            _currentLanguage = "tr";
            return _currentLanguage;
        }

        public void SetCurrentLanguage(string language)
        {
            _currentLanguage = language;
        }
    }
}
