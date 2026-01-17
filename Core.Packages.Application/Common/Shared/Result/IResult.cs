namespace MagicCarRepairAISupported.Application.Shared.Result
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}