using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security;
using System.Text.Json;
using Core.Packages.Domain.Exceptions;
using Core.Packages.Application.Common.Services.Translation;

namespace Core.Packages.Persistence.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly ITranslationService _translationService;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, ITranslationService translationService)
        {
            _next = next;
            _logger = logger;
            _translationService = translationService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError($"Hata yakalandı: {e.Message}");

                if (!httpContext.Response.HasStarted) // ✅ Yanıt başladı mı kontrol et
                {
                    await HandleExceptionAsync(httpContext, e);
                }
                else
                {
                    _logger.LogWarning("Yanıt başlatıldığı için hata mesajı yazılamadı.");
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = e switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                DomainException => (int)HttpStatusCode.BadGateway,
                ApplicationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                SecurityException => StatusCodes.Status401Unauthorized,
                NotSupportedException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            object response;
            if (e is DomainException domainEx)
            {
                string message;
                
                if (domainEx.RequiresTranslation)
                {
                    try
                    {
                        message = await _translationService.GetDomainExceptionMessageAsync(
                            domainEx.ErrorCode!, 
                            domainEx.Parameters, 
                            domainEx.Language);
                    }
                    catch
                    {
                        message = domainEx.Message;
                    }
                }
                else
                {
                    message = domainEx.Message;
                }

                response = new
                {
                    httpContext.Response.StatusCode,
                    Message = message,
                    ErrorCode = domainEx.ErrorCode,
                    Details = domainEx.Details,
                    Language = domainEx.Language,
                    Timestamp = DateTime.UtcNow
                };
            }
            else
            {
                string message = e.Message ?? "Bilinmeyen bir hata oluştu.";
                
                try
                {
                    var translatedMessage = await _translationService.GetTranslationAsync(
                        $"Exception.{e.GetType().Name}", 
                        message);
                    message = translatedMessage;
                }
                catch
                {
                    // Çeviri hatası durumunda orijinal mesajı kullan
                }

                response = new
                {
                    httpContext.Response.StatusCode,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };
            }

            var jsonResponse = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }
}