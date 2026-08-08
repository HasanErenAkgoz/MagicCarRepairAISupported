using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, UpdateMyProfileResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public GetMyProfileQueryHandler(
            ICustomerRepository customerRepository,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService,
            UserManager<UserEntity> userManager,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateMyProfileResponse> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
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

            // Get user information
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // If user is Customer type, try to get Customer record
            if (user.UserType == Domain.Enums.UserType.Customer)
            {
                var currentCustomer = await _customerRepository.GetByUserIdForTenantAsync(
                    userId, clientId.Value, cancellationToken);
                
                // If customer doesn't exist, create one from User data
                if (currentCustomer == null)
                {
                    // Create customer from user data
                    currentCustomer = new Customer
                    {
                        UserId = userId,
                        ClientId = clientId.Value,
                        FirstName = user.FirstName ?? string.Empty,
                        LastName = user.LastName ?? string.Empty,
                        Email = user.Email ?? string.Empty,
                        PhoneNumber = user.PhoneNumber ?? string.Empty,
                        Address = user.Address ?? string.Empty,
                        IdentityNo = user.IdentityNo ?? string.Empty,
                        Language = user.Language ?? "tr",
                        DateTimeOfBirth = DateTime.Now.AddYears(-30) // Default birth date
                    };

                    await _customerRepository.AddAsync(currentCustomer, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

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
            else
            {
                // For Admin, Manager, Employee - return User information directly
                return new UpdateMyProfileResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    FullName = $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Address = user.Address ?? string.Empty,
                    Language = user.Language ?? "tr"
                };
            }
        }
    }
}
