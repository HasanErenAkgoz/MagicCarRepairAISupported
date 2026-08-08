using MagicCarRepairAISupported.Application.Common.Models;
using MagicCarRepairAISupported.Application.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Extensions;

public static class QueryResultExtensions
{
    public static async Task<IActionResult> ToActionResultAsync<T>(
        this QueryResult<T> result,
        IDomainErrorResponseWriter writer,
        HttpContext httpContext,
        CancellationToken cancellationToken = default)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        await writer.WriteAsync(
            httpContext,
            result.ErrorCode!,
            result.Parameters,
            cancellationToken);

        return new EmptyResult();
    }
}
