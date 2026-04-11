using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IDataResult<ForgotPasswordResponse>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISmsService _smsService;
        private readonly IPasswordResetOtpRepository _otpRepository;
        private const int OTP_EXPIRY_MINUTES = 5;
        private const int OTP_LENGTH = 6;

        public ForgotPasswordCommandHandler(
            UserManager<UserEntity> userManager,
            ISmsService smsService,
            IPasswordResetOtpRepository otpRepository)
        {
            _userManager = userManager;
            _smsService = smsService;
            _otpRepository = otpRepository;
        }

        public async Task<IDataResult<ForgotPasswordResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // Telefon numarasını temizle ve formatla
            var cleanPhoneNumber = CleanPhoneNumber(request.PhoneNumber);

            // Kullanıcıyı telefon numarasına göre bul
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == cleanPhoneNumber, cancellationToken);

            // Güvenlik için, kullanıcı bulunamasa bile başarılı yanıt döndür (enumeration önleme)
            if (user == null)
            {
                // Log the attempt for security monitoring
                // _logger.LogWarning($"Forgot password attempt for non-existent phone number: {cleanPhoneNumber}");
                return new SuccessDataResult<ForgotPasswordResponse>(new ForgotPasswordResponse
                {
                    OtpExpiresIn = OTP_EXPIRY_MINUTES * 60,
                    MaskedPhone = MaskPhoneNumber(cleanPhoneNumber)
                }, "OTP code has been sent to your phone number");
            }

            // Rate limiting kontrolü (son 15 dakikada 3'ten fazla OTP isteği var mı?)
            var recentOtps = await _otpRepository.Query()
                .Where(o => o.PhoneNumber == cleanPhoneNumber && o.CreatedDate > DateTime.UtcNow.AddMinutes(-15))
                .CountAsync(cancellationToken);

            if (recentOtps >= 3)
            {
                return new ErrorDataResult<ForgotPasswordResponse>("Too many OTP requests. Please try again later.");
            }

            // Eski OTP'leri geçersiz kıl
            await _otpRepository.InvalidateOldOtpsAsync(user.Id, cancellationToken);

            // OTP oluştur
            var otpCode = GenerateOtp();
            var otpHash = HashOtp(otpCode);

            // OTP kaydı oluştur
            var otp = new PasswordResetOtp
            {
                UserId = user.Id,
                PhoneNumber = cleanPhoneNumber,
                OtpHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(OTP_EXPIRY_MINUTES),
                Used = false,
                Attempts = 0,
                CreatedDate = DateTime.UtcNow
            };

            await _otpRepository.AddAsync(otp, cancellationToken);

            // SMS gönder
            var smsMessage = $"Magic Car Repair - Şifre Sıfırlama\n\nOTP kodunuz: {otpCode}\nBu kod {OTP_EXPIRY_MINUTES} dakika geçerlidir.\n\nEğer bu talebi siz yapmadıysanız, bu mesajı görmezden gelin.\n\nMagic Car Repair";
            var smsSent = await _smsService.SendSmsAsync(cleanPhoneNumber, smsMessage);

            if (!smsSent)
            {
                // SMS gönderilemezse, OTP'yi geçersiz kıl
                otp.Used = true;
                _otpRepository.Update(otp);
                await _otpRepository.SaveChangesAsync();
                return new ErrorDataResult<ForgotPasswordResponse>("Failed to send OTP code. Please try again later.");
            }

            // Telefon numarasını mask'le
            var maskedPhone = MaskPhoneNumber(cleanPhoneNumber);

            var response = new ForgotPasswordResponse
            {
                OtpExpiresIn = OTP_EXPIRY_MINUTES * 60,
                MaskedPhone = maskedPhone
            };

            return new SuccessDataResult<ForgotPasswordResponse>(response, "OTP code has been sent to your phone number");
        }

        private string GenerateOtp()
        {
            var random = new Random();
            var otp = new StringBuilder(OTP_LENGTH);
            for (int i = 0; i < OTP_LENGTH; i++)
            {
                otp.Append(random.Next(0, 10));
            }
            return otp.ToString();
        }

        private string HashOtp(string otp)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(otp));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private string CleanPhoneNumber(string phoneNumber)
        {
            var cleaned = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "");
            if (cleaned.StartsWith("0"))
            {
                cleaned = cleaned.Substring(1);
            }
            if (!cleaned.StartsWith("90"))
            {
                cleaned = "90" + cleaned;
            }
            return cleaned;
        }

        private string MaskPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length < 7)
                return "***";

            // +905551234567 -> +90 5** *** ** 67
            var cleaned = CleanPhoneNumber(phoneNumber);
            if (cleaned.Length >= 10)
            {
                return $"+{cleaned.Substring(0, 2)} {cleaned.Substring(2, 1)}** *** ** {cleaned.Substring(cleaned.Length - 2, 2)}";
            }

            return phoneNumber.Substring(0, 2) + new string('*', phoneNumber.Length - 4) + phoneNumber.Substring(phoneNumber.Length - 2);
        }
    }
}
