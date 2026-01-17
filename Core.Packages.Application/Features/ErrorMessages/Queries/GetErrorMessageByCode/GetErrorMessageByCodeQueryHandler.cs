using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ErrorMessages.Queries.GetErrorMessageByCode
{
    public class GetErrorMessageByCodeQueryHandler : IRequestHandler<GetErrorMessageByCodeQuery, IDataResult<string>>
    {
        private readonly IErrorMessageService _errorMessageService;

        public GetErrorMessageByCodeQueryHandler(IErrorMessageService errorMessageService)
        {
            _errorMessageService = errorMessageService;
        }

        public async Task<IDataResult<string>> Handle(GetErrorMessageByCodeQuery request, CancellationToken cancellationToken)
        {
            var message = await _errorMessageService.GetMessageAsync(
                request.ErrorCode, 
                request.Language, 
                request.Parameters);

            return new SuccessDataResult<string>(message);
        }
    }
}

