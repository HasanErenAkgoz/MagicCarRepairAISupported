using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Exceptions;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Infrastructure.Services;
using FluentValidation;
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
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IErrorMessageService errorMessageService,
            ITenantService tenantService,
            IDomainErrorResponseWriter domainErrorResponseWriter)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await HandleDomainExceptionAsync(context, ex, errorMessageService, domainErrorResponseWriter);
            }
            catch (CustomException ex)
            {
                // Custom exception with localized message support
                await HandleCustomExceptionAsync(context, ex, errorMessageService, tenantService);
            }
            catch (UserFriendlyException ex)
            {
                // User-friendly exception with category
                await HandleUserFriendlyExceptionAsync(context, ex, errorMessageService, tenantService);
            }
            catch (FluentValidation.ValidationException ex)
            {
                // FluentValidation exception
                await HandleValidationExceptionAsync(context, ex, errorMessageService, tenantService);
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

        private async Task HandleDomainExceptionAsync(
            HttpContext context,
            DomainException ex,
            IErrorMessageService errorMessageService,
            IDomainErrorResponseWriter domainErrorResponseWriter)
        {
            var resolvedCode = DomainErrorResponseWriter.ResolveDomainExceptionErrorCode(ex) ?? "UNKNOWN_ERROR";
            var details = ex.Details ?? ex.Parameters;

            if (DomainErrorResponseWriter.IsNotFound(resolvedCode))
                _logger.LogWarning("Domain not found: {ErrorCode}", resolvedCode);
            else
                _logger.LogError(ex, "Domain exception occurred: {ErrorCode}", resolvedCode);

            await domainErrorResponseWriter.WriteAsync(context, resolvedCode, details);
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
            }, _jsonOptions);
            
            await context.Response.WriteAsync(result);
        }

        private async Task HandleUserFriendlyExceptionAsync(HttpContext context, UserFriendlyException ex, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            _logger.LogWarning(ex, "User-friendly exception: {Category} - {Message}", ex.Category, ex.Message);
            
            var language = tenantService.GetCurrentLanguage();
            var localizedMessage = ex.ErrorCode != null 
                ? await errorMessageService.GetMessageAsync(ex.ErrorCode, language, ex.AdditionalData)
                : ex.Message;

            context.Response.ContentType = "application/json";
            
            // Set status code based on category
            context.Response.StatusCode = ex.Category switch
            {
                ErrorCategory.Validation => (int)HttpStatusCode.BadRequest,
                ErrorCategory.Business => (int)HttpStatusCode.BadRequest,
                ErrorCategory.Authentication => (int)HttpStatusCode.Unauthorized,
                ErrorCategory.Authorization => (int)HttpStatusCode.Forbidden,
                ErrorCategory.System => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.BadRequest
            };

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                errorCode = ex.ErrorCode,
                category = ex.Category.ToString(),
                message = localizedMessage,
                additionalData = ex.AdditionalData
            }, _jsonOptions);
            
            await context.Response.WriteAsync(result);
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException ex, IErrorMessageService errorMessageService, ITenantService tenantService)
        {
            _logger.LogWarning(ex, "Validation exception occurred");
            
            var language = tenantService.GetCurrentLanguage();
            
            // Group errors by property name
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                errorCode = "VALIDATION_ERROR",
                message = "Validation failed",
                errors = errors
            }, _jsonOptions);
            
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
            }, _jsonOptions);
            
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
            }, _jsonOptions);
            
            await context.Response.WriteAsync(result);
        }

    }
}
