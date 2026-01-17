using MagicCarRepairAISupported.Application.Common.Services;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Infrastructure.Middlewares
{
    /// <summary>
    /// Middleware to capture and set the tenant context for each request
    /// </summary>
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            // Extract ClientId from header or claims
            var clientId = tenantService.GetCurrentClientId();
            
            // Extract Language from header or claims
            var language = tenantService.GetCurrentLanguage();

            // Set tenant context for this request
            if (clientId.HasValue)
            {
                tenantService.SetCurrentClientId(clientId.Value);
            }

            if (!string.IsNullOrEmpty(language))
            {
                tenantService.SetCurrentLanguage(language);
            }

            await _next(context);
        }
    }
}

