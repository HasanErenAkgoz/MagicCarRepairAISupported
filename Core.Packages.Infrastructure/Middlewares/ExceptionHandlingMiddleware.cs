using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                // Domain exception with localized message support
                await HandleDomainExceptionAsync(context, ex, errorMessageService, tenantService);
            }
            catch (CustomException ex)
            {
                // Custom exception with localized message support
                await HandleCustomExceptionAsync(context, ex, errorMessageService, tenantService);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Unauthorized access
                await HandleUnauthorizedExceptionAsync(context, ex, errorMessageService, tenantService);
            }
            catch (Exception ex)
            {
                // General exception handling
                await HandleGeneralExceptionAsync(context, ex, errorMessageService, tenantService);
            }
        }

        private async Task HandleDomainExceptionAsync(HttpContext context, DomainException ex, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            _logger.LogError(ex, "Domain exception occurred: {ErrorCode}", ex.ErrorCode);
            
            var language = tenantService.GetCurrentLanguage();
            var localizedMessage = await errorMessageService.GetMessageAsync(ex.ErrorCode ?? "UNKNOWN_ERROR", language, ex.Details);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new 
            { 
                success = false,
                errorCode = ex.ErrorCode,
                message = localizedMessage,
                details = ex.Details
            });
            
            await context.Response.WriteAsync(result);
        }

        private async Task HandleCustomExceptionAsync(HttpContext context, CustomException ex, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            _logger.LogError(ex, "Custom exception occurred: {Message}", ex.Message);
            
            var language = tenantService.GetCurrentLanguage();
            // Try to get localized message, fallback to exception message
            var localizedMessage = await errorMessageService.GetMessageAsync(ex.Message, language);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new 
            { 
                success = false,
                message = localizedMessage
            });
            
            await context.Response.WriteAsync(result);
        }

        private async Task HandleUnauthorizedExceptionAsync(HttpContext context, UnauthorizedAccessException ex, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt");
            
            var language = tenantService.GetCurrentLanguage();
            var localizedMessage = await errorMessageService.GetMessageAsync("UNAUTHORIZED_ACCESS", language);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            var result = JsonSerializer.Serialize(new 
            { 
                success = false,
                errorCode = "UNAUTHORIZED_ACCESS",
                message = localizedMessage
            });
            
            await context.Response.WriteAsync(result);
        }

        private async Task HandleGeneralExceptionAsync(HttpContext context, Exception ex, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            
            var language = tenantService.GetCurrentLanguage();
            var localizedMessage = await errorMessageService.GetMessageAsync("SERVER_ERROR", language);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new 
            { 
                success = false,
                errorCode = "SERVER_ERROR",
                message = localizedMessage,
#if DEBUG
                // Include stack trace only in debug mode
                stackTrace = ex.StackTrace
#endif
            });
            
            await context.Response.WriteAsync(result);
        }
    }
}
