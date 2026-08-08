namespace MagicCarRepairAISupported.Application.Common.Models;

/// <summary>
/// MediatR handler sonucu — beklenen iş kuralı hatalarında exception fırlatmak yerine controller yanıt üretir (debugger/test askısı önlenir).
/// </summary>
public sealed class QueryResult<T>
{
    private QueryResult(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private QueryResult(string errorCode, object? parameters)
    {
        IsSuccess = false;
        ErrorCode = errorCode;
        Parameters = parameters;
    }

    public bool IsSuccess { get; }

    public T? Value { get; }

    public string? ErrorCode { get; }

    public object? Parameters { get; }

    public static QueryResult<T> Success(T value) => new(value);

    public static QueryResult<T> NotFound(string errorCode, object? parameters = null) =>
        new(errorCode, parameters);
}
