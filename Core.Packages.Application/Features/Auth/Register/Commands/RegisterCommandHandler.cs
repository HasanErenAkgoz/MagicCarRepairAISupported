using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Roles.Commands.Create;
using MagicCarRepairAISupported.Application.Features.UseRoles.Commands.Create;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Application.Features.Auth.Register.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IDataResult<int>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        
        public RegisterCommandHandler(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            IMediator mediator,
            ITenantService tenantService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            User? existingUser = await _userManager.FindByEmailAsync(request.Email);
            string baseRoleName = "System Admin";

            if (existingUser != null)
            {
                return new ErrorDataResult<int>("User already exists");
            }
            else if (request.Password != request.ConfirmPassword)
            {
                return new ErrorDataResult<int>("The Passwords do not match, please check.");
            }

            // Get current ClientId (default to 1 if not set, for system operations)
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            User user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNo = request.IdentityNo,
                Address = request.Address,
                Email = request.Email,
                UserName = request.Email,
                ClientId = clientId
            };
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            await _mediator.Send(new CreateRoleCommand { Name = baseRoleName });
            await _mediator.Send(new CreateUserRoleCommand { User = user, RoleName = baseRoleName });
            return new SuccessDataResult<int>(user.Id, "User created successfully");
        }
    }
}
