using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdateProfile
{
    public class UpdateClientProfileCommandHandler : IRequestHandler<UpdateClientProfileCommand, UpdateClientProfileResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClientProfileCommandHandler(
            IClientRepository clientRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateClientProfileResponse> Handle(UpdateClientProfileCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var client = await _clientRepository.GetAsync(c => c.Id == clientId, cancellationToken)
                ?? throw new DomainException("CLIENT_NOT_FOUND");

            if (request.Name != null) client.Name = request.Name;
            if (request.Description != null) client.Description = request.Description;
            if (request.AboutUs != null) client.AboutUs = request.AboutUs;
            if (request.ContactEmail != null) client.ContactEmail = request.ContactEmail;
            if (request.ContactPhone != null) client.ContactPhone = request.ContactPhone;
            if (request.Address != null) client.Address = request.Address;
            if (request.WebsiteUrl != null) client.WebsiteUrl = request.WebsiteUrl;
            if (request.LogoUrl != null) client.LogoUrl = request.LogoUrl;
            if (request.WorkingHours != null) client.WorkingHours = request.WorkingHours;
            if (request.Services != null) client.Services = request.Services;
            if (request.SocialMediaLinks != null) client.SocialMediaLinks = request.SocialMediaLinks;
            if (request.IsPublicProfileEnabled.HasValue) client.IsPublicProfileEnabled = request.IsPublicProfileEnabled.Value;

            _clientRepository.Update(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateClientProfileResponse
            {
                Name = client.Name,
                IsPublicProfileEnabled = client.IsPublicProfileEnabled
            };
        }
    }
}
