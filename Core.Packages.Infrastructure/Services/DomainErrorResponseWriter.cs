using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services;

public class DomainErrorResponseWriter : IDomainErrorResponseWriter
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly IErrorMessageService _errorMessageService;
    private readonly ITenantService _tenantService;
    private readonly ILogger<DomainErrorResponseWriter> _logger;

    public DomainErrorResponseWriter(
        IErrorMessageService errorMessageService,
        ITenantService tenantService,
        ILogger<DomainErrorResponseWriter> logger)
    {
        _errorMessageService = errorMessageService;
        _tenantService = tenantService;
        _logger = logger;
    }

    public async Task WriteAsync(
        HttpContext context,
        string errorCode,
        object? details,
        CancellationToken cancellationToken = default)
    {
        if (IsNotFound(errorCode))
            _logger.LogWarning("Domain not found: {ErrorCode}", errorCode);
        else
            _logger.LogWarning("Domain error response: {ErrorCode}", errorCode);

        var language = _tenantService.GetCurrentLanguage();
        var localizedMessage = await _errorMessageService.GetMessageAsync(
            errorCode,
            language,
            details);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ResolveStatusCode(errorCode);

        var payload = JsonSerializer.Serialize(new
        {
            success = false,
            errorCode,
            message = localizedMessage,
            details
        }, JsonOptions);

        await context.Response.WriteAsync(payload, cancellationToken);
    }

    public int ResolveStatusCode(string? errorCode)
    {
        if (string.IsNullOrWhiteSpace(errorCode))
            return (int)HttpStatusCode.BadRequest;

        if (errorCode.EndsWith("_NOT_FOUND", StringComparison.Ordinal) ||
            errorCode.EndsWith("_NOT_FOUND_BY_BARCODE", StringComparison.Ordinal))
            return (int)HttpStatusCode.NotFound;

        if (errorCode.EndsWith("_EXISTS", StringComparison.Ordinal) ||
            errorCode.Contains("_BELONG_TO_CLIENT", StringComparison.Ordinal))
            return (int)HttpStatusCode.Conflict;

        return (int)HttpStatusCode.BadRequest;
    }

    internal static bool IsNotFound(string? errorCode) =>
        !string.IsNullOrWhiteSpace(errorCode) &&
        (errorCode.EndsWith("_NOT_FOUND", StringComparison.Ordinal) ||
         errorCode.EndsWith("_NOT_FOUND_BY_BARCODE", StringComparison.Ordinal));

    internal static string? ResolveDomainExceptionErrorCode(DomainException ex)
    {
        if (!string.IsNullOrEmpty(ex.ErrorCode))
            return ex.ErrorCode;

        var msg = ex.Message;
        if (string.IsNullOrEmpty(msg))
            return null;

        const string prefix = "Translation key: ";
        if (msg.StartsWith(prefix, StringComparison.Ordinal))
            return msg[prefix.Length..].Trim();

        return ErrorCodeLikeMessage.IsMatch(msg) ? msg : null;
    }

    private static readonly Regex ErrorCodeLikeMessage = new(
        "^[A-Z][A-Z0-9_]{2,63}$",
        RegexOptions.CultureInvariant | RegexOptions.Compiled);
}
