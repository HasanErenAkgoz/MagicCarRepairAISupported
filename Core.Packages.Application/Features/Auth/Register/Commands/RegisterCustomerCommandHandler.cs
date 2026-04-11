using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Create;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Register.Commands
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, IDataResult<RegisterCustomerResponse>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        private readonly IVehicleRepository _vehicleRepository;

        public RegisterCustomerCommandHandler(
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager,
            IMediator mediator,
            ITenantService tenantService,
            IVehicleRepository vehicleRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mediator = mediator;
            _tenantService = tenantService;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IDataResult<RegisterCustomerResponse>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Email kontrolü
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new ErrorDataResult<RegisterCustomerResponse>("Bu email adresi zaten kullanılıyor.");
            }

            // 2. Şifre kontrolü
            if (request.Password != request.ConfirmPassword)
            {
                return new ErrorDataResult<RegisterCustomerResponse>("Şifreler eşleşmiyor.");
            }

            // 3. ClientId - Müşteriler için default 1 (genel platform) veya null olabilir
            // Eğer müşteri bir tamirhaneye bağlı değilse, ClientId = 1 (sistem client'ı)
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // 4. User oluştur
            var user = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                IdentityNo = request.IdentityNo ?? string.Empty,
                Language = request.Language, // Kullanıcının dil tercihi
                UserType = UserType.Customer,
                ClientId = clientId
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                return new ErrorDataResult<RegisterCustomerResponse>($"Kullanıcı oluşturulamadı: {errors}");
            }

            // 5. Customer rolünü kontrol et ve yoksa oluştur
            var customerRoleName = "Customer";
            var customerRole = await _roleManager.FindByNameAsync(customerRoleName);
            
            if (customerRole == null)
            {
                customerRole = new Role
                {
                    Name = customerRoleName,
                    NormalizedName = customerRoleName.ToUpper(),
                    ClientId = clientId
                };
                var createRoleResult = await _roleManager.CreateAsync(customerRole);
                if (!createRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return new ErrorDataResult<RegisterCustomerResponse>("Customer rolü oluşturulamadı.");
                }
            }

            // 6. User'a Customer rolünü ata
            var addToRoleResult = await _userManager.AddToRoleAsync(user, customerRoleName);
            if (!addToRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new ErrorDataResult<RegisterCustomerResponse>("Kullanıcıya rol atanamadı.");
            }

            // 7. Customer entity oluştur
            var createCustomerCommand = new CreateCustomerCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNo = request.IdentityNo ?? string.Empty,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                DateTimeOfBirth = request.DateTimeOfBirth ?? new DateTime(1990, 1, 1),
                Language = request.Language,
                IsVip = false,
                UserId = user.Id
            };

            CreateCustomerResponse customerResult;
            try
            {
                customerResult = await _mediator.Send(createCustomerCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user);
                return new ErrorDataResult<RegisterCustomerResponse>($"Müşteri oluşturulamadı: {ex.Message}");
            }

            if (customerResult == null)
            {
                await _userManager.DeleteAsync(user);
                return new ErrorDataResult<RegisterCustomerResponse>("Müşteri oluşturulamadı.");
            }

            // 8. Vehicle data varsa, Vehicle oluştur
            if (!string.IsNullOrWhiteSpace(request.VehicleLicensePlate) && 
                !string.IsNullOrWhiteSpace(request.VehicleBrand) && 
                !string.IsNullOrWhiteSpace(request.VehicleModel))
            {
                // Vehicle oluştur
                var vehicle = new Vehicle
                {
                    CustomerId = customerResult.Id,
                    LicensePlate = request.VehicleLicensePlate,
                    Brand = request.VehicleBrand,
                    Model = request.VehicleModel,
                    Year = request.VehicleYear ?? DateTime.UtcNow.Year,
                    Color = request.VehicleColor ?? "Bilinmiyor",
                    FuelType = request.VehicleFuelType,
                    Status = VehicleStatus.Registered,
                    VehicleType = VehicleType.Unspecified,
                    ClientId = clientId
                };

                if (request.VehicleKilometers.HasValue && request.VehicleKilometers.Value > 0)
                {
                    vehicle.UpdateKilometers(request.VehicleKilometers.Value);
                }

                await _vehicleRepository.AddAsync(vehicle, cancellationToken);
                await _vehicleRepository.SaveChangesAsync();
            }

            // 9. Token oluştur (otomatik login için)
            // Login işlemi için token oluşturulması gerekiyor
            // Bu kısım LoginCommandHandler'daki gibi yapılabilir
            // Şimdilik token'ı boş bırakıyoruz, frontend'de login yapılacak

            var response = new RegisterCustomerResponse
            {
                UserId = user.Id,
                CustomerId = customerResult.Id,
                Email = user.Email,
                Token = string.Empty, // Frontend'de login yapılacak
                Message = "Müşteri hesabı başarıyla oluşturuldu. Giriş yapabilirsiniz."
            };

            return new SuccessDataResult<RegisterCustomerResponse>(response, response.Message);
        }
    }
}
