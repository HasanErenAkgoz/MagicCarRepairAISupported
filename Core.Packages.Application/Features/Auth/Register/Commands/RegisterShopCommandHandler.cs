using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Roles.Commands.Create;
using MagicCarRepairAISupported.Application.Features.UseRoles.Commands.Create;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Register.Commands
{
    public class RegisterShopCommandHandler : IRequestHandler<RegisterShopCommand, IDataResult<RegisterShopResponse>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<RegisterShopCommandHandler> _logger;

        public RegisterShopCommandHandler(
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager,
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IEntityRepository<Customer, int> customerRepository,
            IVehicleRepository vehicleRepository,
            ILogger<RegisterShopCommandHandler> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public async Task<IDataResult<RegisterShopResponse>> Handle(RegisterShopCommand request, CancellationToken cancellationToken)
        {
            // NOTE: Never log passwords or other secrets.
            _logger.LogInformation(
                "RegisterShop started. OwnerEmail={OwnerEmail}, ShopCode={ShopCode}, ShopName={ShopName}",
                request.OwnerEmail,
                request.ShopCode,
                request.ShopName);

            // 1. Email kontrolü
            var existingUser = await _userManager.FindByEmailAsync(request.OwnerEmail);
            if (existingUser != null)
            {
                _logger.LogWarning(
                    "RegisterShop blocked: owner email already exists. OwnerEmail={OwnerEmail}, ExistingUserId={UserId}",
                    request.OwnerEmail,
                    existingUser.Id);
                return new ErrorDataResult<RegisterShopResponse>("Bu email adresi zaten kullanılıyor.");
            }

            // 2. Şifre kontrolü
            if (request.OwnerPassword != request.OwnerConfirmPassword)
            {
                _logger.LogWarning(
                    "RegisterShop blocked: passwords do not match. OwnerEmail={OwnerEmail}, ShopCode={ShopCode}",
                    request.OwnerEmail,
                    request.ShopCode);
                return new ErrorDataResult<RegisterShopResponse>("Şifreler eşleşmiyor.");
            }

            // 3. Shop Code kontrolü
            var isCodeUnique = await _clientRepository.IsCodeUniqueAsync(request.ShopCode);
            if (!isCodeUnique)
            {
                _logger.LogWarning(
                    "RegisterShop blocked: shop code already exists. ShopCode={ShopCode}, OwnerEmail={OwnerEmail}",
                    request.ShopCode,
                    request.OwnerEmail);
                return new ErrorDataResult<RegisterShopResponse>($"'{request.ShopCode}' kodu zaten kullanılıyor. Lütfen farklı bir kod seçin.");
            }

            // 4. Client (Shop) oluştur - IsActive = false (admin onayı bekliyor)
            var client = new Client
            {
                Name = request.ShopName,
                Code = request.ShopCode,
                Description = request.ShopDescription,
                ContactEmail = request.ShopContactEmail,
                ContactPhone = request.ShopContactPhone,
                Address = request.ShopAddress,
                TaxOfficeNo = request.TaxOfficeNo,
                IsActive = false, // Admin onayı bekliyor
                IsPublicProfileEnabled = false,
                LogoUrl = request.ShopLogoUrl,
                BannerUrl = request.ShopBannerUrl,
                Latitude = request.ShopLatitude,
                Longitude = request.ShopLongitude
            };

            await _clientRepository.AddAsync(client, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(
                "RegisterShop client created. ClientId={ClientId}, ShopCode={ShopCode}, OwnerEmail={OwnerEmail}",
                client.Id,
                client.Code,
                request.OwnerEmail);

            // 5. Owner User oluştur
            var ownerUser = new UserEntity
            {
                FirstName = request.OwnerFirstName,
                LastName = request.OwnerLastName,
                Email = request.OwnerEmail,
                UserName = request.OwnerEmail,
                PhoneNumber = request.OwnerPhoneNumber,
                IdentityNo = request.OwnerIdentityNo,  // nullable artık
                Address = request.OwnerAddress,
                UserType = UserType.Manager,
                ClientId = client.Id
            };

            var createUserResult = await _userManager.CreateAsync(ownerUser, request.OwnerPassword);
            if (!createUserResult.Succeeded)
            {
                // Client'ı sil
                _clientRepository.Delete(client);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                _logger.LogWarning(
                    "RegisterShop failed: user create failed. OwnerEmail={OwnerEmail}, ShopCode={ShopCode}, Errors={Errors}",
                    request.OwnerEmail,
                    request.ShopCode,
                    errors);
                return new ErrorDataResult<RegisterShopResponse>($"Kullanıcı oluşturulamadı: {errors}");
            }
            _logger.LogInformation(
                "RegisterShop owner user created. OwnerUserId={OwnerUserId}, OwnerEmail={OwnerEmail}, ClientId={ClientId}",
                ownerUser.Id,
                ownerUser.Email,
                client.Id);

            // 6. Manager rolünü kontrol et ve yoksa oluştur
            var managerRoleName = "Manager";
            var managerRole = await _roleManager.FindByNameAsync(managerRoleName);
            
            if (managerRole == null)
            {
                managerRole = new Role
                {
                    Name = managerRoleName,
                    NormalizedName = managerRoleName.ToUpper(),
                    ClientId = client.Id
                };
                var createRoleResult = await _roleManager.CreateAsync(managerRole);
                if (!createRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(ownerUser);
                    _clientRepository.Delete(client);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    
                    _logger.LogWarning(
                        "RegisterShop failed: manager role could not be created. OwnerEmail={OwnerEmail}, ClientId={ClientId}",
                        request.OwnerEmail,
                        client.Id);
                    return new ErrorDataResult<RegisterShopResponse>("Manager rolü oluşturulamadı.");
                }
            }

            // 7. Owner'a Manager rolünü ata
            var addToRoleResult = await _userManager.AddToRoleAsync(ownerUser, managerRoleName);
            if (!addToRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(ownerUser);
                _clientRepository.Delete(client);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                var roleErrors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                _logger.LogWarning(
                    "RegisterShop failed: could not assign Manager role. OwnerEmail={OwnerEmail}, OwnerUserId={OwnerUserId}, Errors={Errors}",
                    request.OwnerEmail,
                    ownerUser.Id,
                    roleErrors);
                return new ErrorDataResult<RegisterShopResponse>("Kullanıcıya rol atanamadı.");
            }
            _logger.LogInformation(
                "RegisterShop completed: role assigned. OwnerUserId={OwnerUserId}, ClientId={ClientId}",
                ownerUser.Id,
                client.Id);

            // 8. Vehicle data varsa, önce Customer oluştur sonra Vehicle oluştur
            if (!string.IsNullOrWhiteSpace(request.VehicleLicensePlate) && 
                !string.IsNullOrWhiteSpace(request.VehicleBrand) && 
                !string.IsNullOrWhiteSpace(request.VehicleModel))
            {
                // Owner için Customer kaydı oluştur
                var ownerCustomer = new Customer
                {
                    IdentityNo = request.OwnerIdentityNo ?? string.Empty,
                    FirstName = request.OwnerFirstName,
                    LastName = request.OwnerLastName,
                    Email = request.OwnerEmail,
                    PhoneNumber = request.OwnerPhoneNumber ?? string.Empty,
                    Address = request.OwnerAddress ?? string.Empty,
                    DateTimeOfBirth = DateTime.UtcNow.AddYears(-30), // Default, gerçek doğum tarihi bilinmiyorsa
                    ClientId = client.Id,
                    UserId = ownerUser.Id
                };

                await _customerRepository.AddAsync(ownerCustomer, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Vehicle oluştur
                var vehicle = new Vehicle
                {
                    CustomerId = ownerCustomer.Id,
                    LicensePlate = request.VehicleLicensePlate,
                    Brand = request.VehicleBrand,
                    Model = request.VehicleModel,
                    Year = request.VehicleYear ?? DateTime.UtcNow.Year,
                    Color = request.VehicleColor ?? "Bilinmiyor",
                    Status = VehicleStatus.Registered,
                    VehicleType = VehicleType.Unspecified,
                    ClientId = client.Id
                };

                if (request.VehicleKilometers.HasValue && request.VehicleKilometers.Value > 0)
                {
                    vehicle.UpdateKilometers(request.VehicleKilometers.Value);
                }

                await _vehicleRepository.AddAsync(vehicle, cancellationToken);
                await _vehicleRepository.SaveChangesAsync();
            }

            var response = new RegisterShopResponse
            {
                ClientId = client.Id,
                OwnerUserId = ownerUser.Id,
                ShopName = client.Name,
                OwnerEmail = ownerUser.Email,
                Message = "Tamirhane kaydı başarıyla oluşturuldu. Admin onayı bekleniyor. Onaylandıktan sonra giriş yapabilirsiniz."
            };

            return new SuccessDataResult<RegisterShopResponse>(response, response.Message);
        }
    }
}
