using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile
{
    public class UpdateMyProfileCommandHandler : IRequestHandler<UpdateMyProfileCommand, UpdateMyProfileResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public UpdateMyProfileCommandHandler(
            ICustomerRepository customerRepository,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        public async Task<UpdateMyProfileResponse> Handle(UpdateMyProfileCommand request, CancellationToken cancellationToken)
        {
            // Get current user ID from HttpContext
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
            {
                throw new UnauthorizedAccessException("Client ID not found");
            }

            // Find customer by UserId
            var customers = await _customerRepository.GetListAsync(cancellationToken, c => c.UserId == userId && c.ClientId == clientId.Value);
            var currentCustomer = customers.FirstOrDefault();
            
            if (currentCustomer == null)
            {
                throw new UnauthorizedAccessException("Customer not found");
            }

            // Update customer properties
            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                currentCustomer.FirstName = request.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                currentCustomer.LastName = request.LastName;
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                currentCustomer.PhoneNumber = request.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(request.Address))
            {
                currentCustomer.Address = request.Address;
            }

            if (!string.IsNullOrWhiteSpace(request.Language))
            {
                currentCustomer.Language = request.Language;
            }

            _customerRepository.Update(currentCustomer);
            await _customerRepository.SaveChangesAsync();

            return new UpdateMyProfileResponse
            {
                Id = currentCustomer.Id,
                FirstName = currentCustomer.FirstName,
                LastName = currentCustomer.LastName,
                FullName = currentCustomer.FullName,
                Email = currentCustomer.Email,
                PhoneNumber = currentCustomer.PhoneNumber,
                Address = currentCustomer.Address,
                Language = currentCustomer.Language
            };
        }
    }
}

