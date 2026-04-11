using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IPasswordResetOtpRepository _otpRepository;
        private readonly ISmsService _smsService;

        public ResetPasswordCommandHandler(
            UserManager<UserEntity> userManager,
            IPasswordResetOtpRepository otpRepository,
            ISmsService smsService)
        {
            _userManager = userManager;
            _otpRepository = otpRepository;
            _smsService = smsService;
        }

        public async Task<IResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var cleanPhoneNumber = CleanPhoneNumber(request.PhoneNumber);

            var otpEntry = await _otpRepository.GetByResetTokenAsync(request.ResetToken, cancellationToken);

            if (otpEntry == null || otpEntry.Used || otpEntry.ResetTokenExpiresAt < DateTime.UtcNow || otpEntry.PhoneNumber != cleanPhoneNumber)
            {
                return new ErrorResult("Invalid or expired reset token.");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == otpEntry.UserId, cancellationToken);
            if (user == null)
            {
                return new ErrorResult("User not found.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return new ErrorResult("Passwords do not match.");
            }

            // Şifre validasyonu (UserManager tarafından otomatik yapılır)
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!resetResult.Succeeded)
            {
                return new ErrorResult(string.Join(", ", resetResult.Errors.Select(e => e.Description)));
            }

            // Reset token'ı kullanıldı olarak işaretle
            otpEntry.Used = true;
            _otpRepository.Update(otpEntry);
            await _otpRepository.SaveChangesAsync();

            // Kullanıcıya şifre değişikliği bildirimi gönder
            var smsMessage = "Magic Car Repair\n\nŞifreniz başarıyla değiştirildi.\nEğer bu işlemi siz yapmadıysanız, lütfen derhal bizimle iletişime geçin.\n\nMagic Car Repair";
            await _smsService.SendSmsAsync(cleanPhoneNumber, smsMessage);

            return new SuccessResult("Password has been reset successfully");
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
