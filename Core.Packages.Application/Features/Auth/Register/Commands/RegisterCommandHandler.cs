using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Roles.Commands.Create;
using MagicCarRepairAISupported.Application.Features.UseRoles.Commands.Create;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Register.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IDataResult<int>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        private readonly IClientRepository _clientRepository;
        
        public RegisterCommandHandler(
            UserManager<UserEntity> userManager, 
            RoleManager<Role> roleManager, 
            IMediator mediator,
            ITenantService tenantService,
            IClientRepository clientRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _tenantService = tenantService;
            _clientRepository = clientRepository;
        }

        public async Task<IDataResult<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            UserEntity? existingUser = await _userManager.FindByEmailAsync(request.Email);
            string baseRoleName = "System Admin";

            if (existingUser != null)
            {
                return new ErrorDataResult<int>("User already exists");
            }
            else if (request.Password != request.ConfirmPassword)
            {
                return new ErrorDataResult<int>("The Passwords do not match, please check.");
            }

            var resolvedClientId = _tenantService.GetClientIdOrDefault();
            var clientRow = await _clientRepository.GetByIdAsync(resolvedClientId, cancellationToken);
            if (clientRow == null)
            {
                return new ErrorDataResult<int>("Geçerli bir servis (client) bulunamadı.");
            }

            var clientId = clientRow.Id;

            UserEntity user = new UserEntity
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
