using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, IDataResult<GetClientByIdResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IErrorMessageService _errorMessageService;

        public GetClientByIdQueryHandler(
            IClientRepository clientRepository,
            IMapper mapper,
            IErrorMessageService errorMessageService)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _errorMessageService = errorMessageService;
        }

        public async Task<IDataResult<GetClientByIdResponse>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetAsync(c => c.Id == request.Id, cancellationToken);
            
            if (client == null)
            {
                var errorMessage = await _errorMessageService.GetMessageAsync("CLIENT_NOT_FOUND");
                return new ErrorDataResult<GetClientByIdResponse>(errorMessage);
            }

            var response = _mapper.Map<GetClientByIdResponse>(client);
            return new SuccessDataResult<GetClientByIdResponse>(response);
        }
    }
}

