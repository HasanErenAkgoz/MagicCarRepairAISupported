using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands
{
    public class VerifyResetOtpCommandHandler : IRequestHandler<VerifyResetOtpCommand, IDataResult<VerifyResetOtpResponse>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IPasswordResetOtpRepository _otpRepository;
        private const int MAX_OTP_ATTEMPTS = 3;
        private const int RESET_TOKEN_EXPIRY_MINUTES = 10;

        public VerifyResetOtpCommandHandler(UserManager<UserEntity> userManager, IPasswordResetOtpRepository otpRepository)
        {
            _userManager = userManager;
            _otpRepository = otpRepository;
        }

        public async Task<IDataResult<VerifyResetOtpResponse>> Handle(VerifyResetOtpCommand request, CancellationToken cancellationToken)
        {
            var cleanPhoneNumber = CleanPhoneNumber(request.PhoneNumber);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == cleanPhoneNumber, cancellationToken);
            if (user == null)
            {
                return new ErrorDataResult<VerifyResetOtpResponse>("User with this phone number not found.");
            }

            var latestOtp = await _otpRepository.GetLatestOtpForUserAsync(user.Id, cleanPhoneNumber, cancellationToken);

            if (latestOtp == null || latestOtp.Used || latestOtp.ExpiresAt < DateTime.UtcNow)
            {
                return new ErrorDataResult<VerifyResetOtpResponse>("Invalid or expired OTP code.");
            }

            if (latestOtp.Attempts >= MAX_OTP_ATTEMPTS)
            {
                latestOtp.Used = true;
                _otpRepository.Update(latestOtp);
                await _otpRepository.SaveChangesAsync();
                return new ErrorDataResult<VerifyResetOtpResponse>("Too many failed attempts. Please request a new OTP code.");
            }

            var hashedInputOtp = HashOtp(request.Otp);

            if (hashedInputOtp != latestOtp.OtpHash)
            {
                latestOtp.Attempts++;
                _otpRepository.Update(latestOtp);
                await _otpRepository.SaveChangesAsync();
                return new ErrorDataResult<VerifyResetOtpResponse>("Invalid OTP code.");
            }

            // OTP doğrulandı, tek kullanımlık yap
            latestOtp.Used = true;

            // Reset token oluştur
            var resetToken = GenerateResetToken();
            latestOtp.ResetToken = resetToken;
            latestOtp.ResetTokenExpiresAt = DateTime.UtcNow.AddMinutes(RESET_TOKEN_EXPIRY_MINUTES);

            _otpRepository.Update(latestOtp);
            await _otpRepository.SaveChangesAsync();

            return new SuccessDataResult<VerifyResetOtpResponse>(new VerifyResetOtpResponse
            {
                ResetToken = resetToken,
                ResetTokenExpiresIn = RESET_TOKEN_EXPIRY_MINUTES * 60
            }, "OTP verified successfully");
        }

        private string HashOtp(string otp)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(otp));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private string GenerateResetToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)); // 64 bytes for a strong token
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
    }
}
