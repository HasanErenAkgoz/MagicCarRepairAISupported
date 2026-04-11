using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, IDataResult<CreateClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IErrorMessageService _errorMessageService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public CreateClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IErrorMessageService errorMessageService,
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _errorMessageService = errorMessageService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IDataResult<CreateClientResponse>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            // 1. Code benzersizlik kontrolü
            var isUnique = await _clientRepository.IsCodeUniqueAsync(request.Code);
            if (!isUnique)
            {
                var codeError = await _errorMessageService.GetMessageAsync("CLIENT_CODE_EXISTS", parameters: new { Code = request.Code });
                return new ErrorDataResult<CreateClientResponse>(codeError);
            }

            // 2. Yönetici e-posta kontrolü
            var existingUser = await _userManager.FindByEmailAsync(request.OwnerEmail);
            if (existingUser != null)
            {
                return new ErrorDataResult<CreateClientResponse>("Bu e-posta adresi zaten kullanılıyor.");
            }

            // 3. Client oluştur (IsActive = false — onay bekliyor)
            var client = _mapper.Map<Client>(request);
            client.IsActive = false;

            await _clientRepository.AddAsync(client, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Manager kullanıcısı oluştur
            UserEntity ownerUser;
            try
            {
                ownerUser = new UserEntity
                {
                    FirstName = request.OwnerFirstName,
                    LastName = request.OwnerLastName,
                    Email = request.OwnerEmail,
                    UserName = request.OwnerEmail,
                    PhoneNumber = request.OwnerPhoneNumber,
                    UserType = UserType.Manager,
                    ClientId = client.Id
                };

                var createUserResult = await _userManager.CreateAsync(ownerUser, request.OwnerPassword);
                if (!createUserResult.Succeeded)
                {
                    _clientRepository.Delete(client);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                    return new ErrorDataResult<CreateClientResponse>($"Kullanıcı oluşturulamadı: {errors}");
                }
            }
            catch (Exception)
            {
                // Kullanıcı oluşturma beklenmeyen hata aldı — client'ı geri al
                try { _clientRepository.Delete(client); await _unitOfWork.SaveChangesAsync(cancellationToken); } catch { /* best-effort */ }
                return new ErrorDataResult<CreateClientResponse>("Yönetici kullanıcısı oluşturulurken bir hata oluştu. Lütfen tekrar deneyin.");
            }

            // 5. Manager rolünü kontrol et / oluştur
            const string managerRoleName = "Manager";
            if (!await _roleManager.RoleExistsAsync(managerRoleName))
            {
                var role = new Role
                {
                    Name = managerRoleName,
                    NormalizedName = managerRoleName.ToUpper(),
                    ClientId = client.Id
                };
                var createRoleResult = await _roleManager.CreateAsync(role);
                if (!createRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(ownerUser);
                    _clientRepository.Delete(client);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    return new ErrorDataResult<CreateClientResponse>("Manager rolü oluşturulamadı.");
                }
            }

            // 6. Kullanıcıya Manager rolünü ata
            var addRoleResult = await _userManager.AddToRoleAsync(ownerUser, managerRoleName);
            if (!addRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(ownerUser);
                _clientRepository.Delete(client);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return new ErrorDataResult<CreateClientResponse>("Kullanıcıya rol atanamadı.");
            }

            var response = _mapper.Map<CreateClientResponse>(client);
            response.OwnerUserId = ownerUser.Id;
            response.OwnerEmail = ownerUser.Email!;

            var successMessage = await _errorMessageService.GetMessageAsync(Messages.Added);
            return new SuccessDataResult<CreateClientResponse>(response, successMessage);
        }
    }
}
