using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Common.Services;

/// <summary>
/// Domain hata kodlarını ExceptionHandlingMiddleware ile aynı JSON/status ile yazar.
/// </summary>
public interface IDomainErrorResponseWriter
{
    Task WriteAsync(
        HttpContext context,
        string errorCode,
        object? details,
        CancellationToken cancellationToken = default);

    int ResolveStatusCode(string? errorCode);
}
