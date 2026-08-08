using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.CreateUserByAdmin
{
    public class CreateUserByAdminCommandHandler : IRequestHandler<CreateUserByAdminCommand, IResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<CreateUserByAdminCommandHandler> _logger;

        public CreateUserByAdminCommandHandler(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IClientRepository clientRepository,
            ILogger<CreateUserByAdminCommandHandler> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IResult> Handle(CreateUserByAdminCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(UserType), request.UserType))
                return new ErrorResult("Geçersiz kullanıcı tipi. (2=Manager, 3=Employee)");

            if (request.ClientId <= 0)
                return new ErrorResult("Geçersiz servis (ClientId). Pozitif bir değer gerekli.");

            var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
                return new ErrorResult("Belirtilen servis (client) bulunamadı. Var olan bir ClientId kullanın.");

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return new ErrorResult("Bu e-posta adresi zaten kullanılıyor.");

            var userType = (UserType)request.UserType;

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNo = string.Empty,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserType = userType,
                ClientId = request.ClientId
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                return new ErrorResult($"Kullanıcı oluşturulamadı: {errors}");
            }

            // Assign identity role based on UserType
            var roleName = userType switch
            {
                UserType.Manager => "Manager",
                UserType.Employee => "Employee",
                _ => null
            };

            if (roleName != null)
            {
                // Ensure role exists
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        ClientId = request.ClientId
                    });
                }

                await _userManager.AddToRoleAsync(user, roleName);
            }

            _logger.LogInformation(
                "User created by admin: UserId={UserId}, Email={Email}, UserType={UserType}, ClientId={ClientId}",
                user.Id, user.Email, userType, request.ClientId);

            return new SuccessResult("Kullanıcı başarıyla oluşturuldu.");
        }
    }
}
