using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Enable2FA
{
    public class Enable2FACommandHandler : IRequestHandler<Enable2FACommand, IDataResult<Enable2FAResponse>>
    {
        private readonly ITwoFactorService _twoFactorService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Enable2FACommandHandler(
            ITwoFactorService twoFactorService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _twoFactorService = twoFactorService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Enable2FAResponse>> Handle(Enable2FACommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                {
                    return new ErrorDataResult<Enable2FAResponse>("User not found.");
                }

                // Zaten aktif ise hata döndür
                if (user.TwoFactorEnabled)
                {
                    return new ErrorDataResult<Enable2FAResponse>("Two-factor authentication is already enabled.");
                }

                // 2FA secret oluştur
                var setupResult = await _twoFactorService.GenerateSecretAsync(user.Email ?? user.UserName ?? "user");

                // Recovery code'ları oluştur
                var recoveryCodes = await _twoFactorService.GenerateRecoveryCodesAsync(10);

                // User'a secret ve recovery code'ları kaydet (henüz aktif değil, doğrulama sonrası aktif olacak)
                user.TwoFactorSecret = setupResult.Secret;
                user.RecoveryCodes = JsonSerializer.Serialize(recoveryCodes);
                // TwoFactorEnabled = false (doğrulama sonrası true yapılacak)

                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new Enable2FAResponse
                {
                    RequiresTwoFactor = false, // Henüz aktif değil, verify sonrası true olacak
                    QrCodeUrl = setupResult.QrCodeUrl,
                    BackupCodes = recoveryCodes, // Sadece bir kez gösterilecek
                    Secret = setupResult.Secret, // For backward compatibility
                    ManualEntryKey = setupResult.ManualEntryKey, // For backward compatibility
                    RecoveryCodes = recoveryCodes, // For backward compatibility
                    Message = "2FA kurulumu başlatıldı. Lütfen QR kodu tarayın veya manuel kodu girin, ardından doğrulama kodunu girin."
                };

                return new SuccessDataResult<Enable2FAResponse>(response, "Two-factor authentication has been enabled successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Enable2FAResponse>($"2FA kurulumu sırasında hata oluştu: {ex.Message}");
            }
        }
    }
}
