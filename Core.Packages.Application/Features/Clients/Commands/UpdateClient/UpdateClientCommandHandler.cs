using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, IDataResult<UpdateClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorMessageService _errorMessageService;

        public UpdateClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IErrorMessageService errorMessageService)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _errorMessageService = errorMessageService;
        }

        public async Task<IDataResult<UpdateClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (client == null)
            {
                var notFoundMessage = await _errorMessageService.GetMessageAsync(Messages.NotFound);
                return new ErrorDataResult<UpdateClientResponse>(notFoundMessage);
            }

            // If a new code is provided and differs from current, verify uniqueness
            if (request.Code != null && request.Code != client.Code)
            {
                var isUnique = await _clientRepository.IsCodeUniqueAsync(request.Code, excludeId: client.Id);
                if (!isUnique)
                {
                    var errorMessage = await _errorMessageService.GetMessageAsync("CLIENT_CODE_EXISTS", parameters: new { Code = request.Code });
                    return new ErrorDataResult<UpdateClientResponse>(errorMessage);
                }
                client.Code = request.Code;
            }

            // Patch-style: only update fields that were provided
            if (request.Name != null)
                client.Name = request.Name;
            if (request.Description != null)
                client.Description = request.Description;
            if (request.ContactEmail != null)
                client.ContactEmail = request.ContactEmail;
            if (request.ContactPhone != null)
                client.ContactPhone = request.ContactPhone;
            if (request.Address != null)
                client.Address = request.Address;
            if (request.LogoUrl != null)
                client.LogoUrl = request.LogoUrl;
            if (request.BannerUrl != null)
                client.BannerUrl = request.BannerUrl;
            if (request.Latitude.HasValue)
                client.Latitude = request.Latitude.Value;
            if (request.Longitude.HasValue)
                client.Longitude = request.Longitude.Value;
            if (request.SubscriptionStartDate.HasValue)
                client.SubscriptionStartDate = request.SubscriptionStartDate.Value;
            if (request.SubscriptionEndDate.HasValue)
                client.SubscriptionEndDate = request.SubscriptionEndDate.Value;

            _clientRepository.Update(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var successMessage = await _errorMessageService.GetMessageAsync(Messages.Updated);

            var response = new UpdateClientResponse
            {
                Id = client.Id,
                Name = client.Name,
                Message = successMessage
            };

            return new SuccessDataResult<UpdateClientResponse>(response, successMessage);
        }
    }
}
