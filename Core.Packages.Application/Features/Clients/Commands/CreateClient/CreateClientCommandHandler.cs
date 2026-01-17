using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, IDataResult<CreateClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IErrorMessageService _errorMessageService;

        public CreateClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IErrorMessageService errorMessageService)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _errorMessageService = errorMessageService;
        }

        public async Task<IDataResult<CreateClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            // Check if code is unique
            var isUnique = await _clientRepository.IsCodeUniqueAsync(request.Code);
            if (!isUnique)
            {
                var errorMessage = await _errorMessageService.GetMessageAsync("CLIENT_CODE_EXISTS", parameters: new { Code = request.Code });
                return new ErrorDataResult<CreateClientResponse>(errorMessage);
            }

            var client = _mapper.Map<Client>(request);
            client.IsActive = true;

            await _clientRepository.AddAsync(client, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CreateClientResponse>(client);
            var successMessage = await _errorMessageService.GetMessageAsync(Messages.Added);
            
            return new SuccessDataResult<CreateClientResponse>(response, successMessage);
        }
    }
}

