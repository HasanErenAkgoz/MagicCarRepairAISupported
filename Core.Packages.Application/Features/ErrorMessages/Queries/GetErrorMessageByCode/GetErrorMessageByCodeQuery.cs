using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ErrorMessages.Queries.GetErrorMessageByCode
{
    public class GetErrorMessageByCodeQuery : IRequest<IDataResult<string>>
    {
        public string ErrorCode { get; set; }
        public string? Language { get; set; }
        public object? Parameters { get; set; }
    }
}

