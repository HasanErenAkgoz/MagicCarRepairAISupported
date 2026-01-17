using MagicCarRepairAISupported.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MagicCarRepairAISupported.Persistence.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static void UseCustomMiddlewares(this IApplicationBuilder app)
        {
            // Multi-tenant middleware - must be before other middlewares
            app.UseMiddleware<TenantMiddleware>();
            
            // Exception handling with localization support
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
