using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.ForgotPassword.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(UserManager<UserEntity> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new ErrorResult("Bu email adresine sahip kullan�c� bulunamad�.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"https://localhost:7216/api/Auth/reset-password?email={request.Email}&token={WebUtility.UrlEncode(token)}";

            var emailSent = await _emailService.SendEmailAsync(request.Email, "�ifre S�f�rlama", $"�ifrenizi s�f�rlamak i�in <a href='{resetLink}'>buraya t�klay�n</a>");

            if (!emailSent)
                return new ErrorResult("�ifre s�f�rlama emaili g�nderilemedi.");

            return new SuccessResult("�ifre s�f�rlama ba�lant�s� email adresinize g�nderildi.");
        }
    }

}
