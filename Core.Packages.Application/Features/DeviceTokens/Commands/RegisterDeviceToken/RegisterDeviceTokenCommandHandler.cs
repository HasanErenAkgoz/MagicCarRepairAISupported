using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Features.DeviceTokens.Commands.RegisterDeviceToken
{
    public class RegisterDeviceTokenCommandHandler : IRequestHandler<RegisterDeviceTokenCommand, IDataResult<RegisterDeviceTokenResponse>>
    {
        private readonly IUserDeviceTokenRepository _deviceTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterDeviceTokenCommandHandler(
            IUserDeviceTokenRepository deviceTokenRepository,
            IUserRepository userRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _deviceTokenRepository = deviceTokenRepository;
            _userRepository = userRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<RegisterDeviceTokenResponse>> Handle(RegisterDeviceTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // User ID'yi HttpContext'ten al
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return new ErrorDataResult<RegisterDeviceTokenResponse>("Kullanıcı bilgisi bulunamadı.");
                }

                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Token zaten var mı kontrol et
                var existingToken = await _deviceTokenRepository.GetByTokenAsync(request.Token, cancellationToken);
                
                if (existingToken != null)
                {
                    // Token varsa güncelle
                    existingToken.UserId = userId;
                    existingToken.DeviceType = request.DeviceType;
                    existingToken.DeviceModel = request.DeviceModel;
                    existingToken.DeviceOSVersion = request.DeviceOSVersion;
                    existingToken.IsActive = true;
                    existingToken.LastUsedDate = DateTime.UtcNow;
                    existingToken.ClientId = clientId;
                    
                    _deviceTokenRepository.Update(existingToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new SuccessDataResult<RegisterDeviceTokenResponse>(
                        new RegisterDeviceTokenResponse
                        {
                            DeviceTokenId = existingToken.Id,
                            Message = "Cihaz token'ı güncellendi."
                        },
                        "Cihaz token'ı güncellendi."
                    );
                }

                // Yeni token oluştur
                var deviceToken = new UserDeviceToken
                {
                    UserId = userId,
                    Token = request.Token,
                    DeviceType = request.DeviceType,
                    DeviceModel = request.DeviceModel,
                    DeviceOSVersion = request.DeviceOSVersion,
                    IsActive = true,
                    LastUsedDate = DateTime.UtcNow,
                    ClientId = clientId
                };

                await _deviceTokenRepository.AddAsync(deviceToken, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<RegisterDeviceTokenResponse>(
                    new RegisterDeviceTokenResponse
                    {
                        DeviceTokenId = deviceToken.Id,
                        Message = "Cihaz token'ı kaydedildi."
                    },
                    "Cihaz token'ı başarıyla kaydedildi."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<RegisterDeviceTokenResponse>($"Cihaz token'ı kaydedilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
