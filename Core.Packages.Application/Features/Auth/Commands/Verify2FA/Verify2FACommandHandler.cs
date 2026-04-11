using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Verify2FA
{
    public class Verify2FACommandHandler : IRequestHandler<Verify2FACommand, IDataResult<Verify2FAResponse>>
    {
        private readonly ITwoFactorService _twoFactorService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Verify2FACommandHandler(
            ITwoFactorService twoFactorService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _twoFactorService = twoFactorService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Verify2FAResponse>> Handle(Verify2FACommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                {
                    return new ErrorDataResult<Verify2FAResponse>("Kullanıcı bulunamadı.");
                }

                if (string.IsNullOrEmpty(user.TwoFactorSecret))
                {
                    return new ErrorDataResult<Verify2FAResponse>("2FA henüz kurulmamış.");
                }

                // TOTP kodunu doğrula
                var isValid = await _twoFactorService.VerifyCodeAsync(user.TwoFactorSecret, request.Code);

                if (!isValid)
                {
                    // Recovery code kontrolü
                    if (!string.IsNullOrEmpty(user.RecoveryCodes))
                    {
                        var recoveryCodes = System.Text.Json.JsonSerializer.Deserialize<List<string>>(user.RecoveryCodes) ?? new List<string>();
                        var recoveryValid = await _twoFactorService.VerifyRecoveryCodeAsync(recoveryCodes, request.Code);
                        
                        if (recoveryValid)
                        {
                            // Recovery code kullanıldı, listeyi güncelle
                            user.RecoveryCodes = System.Text.Json.JsonSerializer.Serialize(recoveryCodes);
                            _userRepository.Update(user);
                            await _unitOfWork.SaveChangesAsync(cancellationToken);
                            
                            return new SuccessDataResult<Verify2FAResponse>(
                                new Verify2FAResponse
                                {
                                    IsValid = true,
                                    TwoFactorEnabled = user.TwoFactorEnabled,
                                    RequiresTwoFactor = user.TwoFactorEnabled,
                                    Message = "Recovery code ile doğrulama başarılı."
                                },
                                "Recovery code ile doğrulama başarılı."
                            );
                        }
                    }

                    return new ErrorDataResult<Verify2FAResponse>(
                        new Verify2FAResponse
                        {
                            IsValid = false,
                            TwoFactorEnabled = user.TwoFactorEnabled,
                            RequiresTwoFactor = user.TwoFactorEnabled,
                            Message = "Geçersiz kod. Lütfen tekrar deneyin."
                        },
                        "Geçersiz kod."
                    );
                }

                // Setup sırasında ise 2FA'yı aktif et
                if (request.IsSetup && !user.TwoFactorEnabled)
                {
                    user.TwoFactorEnabled = true;
                    _userRepository.Update(user);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return new SuccessDataResult<Verify2FAResponse>(
                    new Verify2FAResponse
                    {
                        IsValid = true,
                        TwoFactorEnabled = user.TwoFactorEnabled,
                        RequiresTwoFactor = user.TwoFactorEnabled,
                        Message = request.IsSetup ? "Two-factor authentication has been enabled successfully." : "2FA doğrulaması başarılı."
                    },
                    request.IsSetup ? "Two-factor authentication has been enabled successfully." : "2FA doğrulaması başarılı."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<Verify2FAResponse>($"2FA doğrulaması sırasında hata oluştu: {ex.Message}");
            }
        }
    }
}
