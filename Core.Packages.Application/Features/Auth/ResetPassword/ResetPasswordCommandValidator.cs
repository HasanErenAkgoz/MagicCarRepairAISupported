using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alan� bo� olamaz.")
                .EmailAddress().WithMessage("Ge�erli bir email adresi giriniz.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token alan� bo� olamaz.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni �ifre alan� bo� olamaz.")
                .MinimumLength(8).WithMessage("�ifre en az 8 karakter olmal�d�r.")
                .Matches("[A-Z]").WithMessage("�ifre en az bir b�y�k harf i�ermelidir.")
                .Matches("[a-z]").WithMessage("�ifre en az bir k���k harf i�ermelidir.")
                .Matches("[0-9]").WithMessage("�ifre en az bir rakam i�ermelidir.")
                .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("�ifre en az bir �zel karakter i�ermelidir.");
        }
    }
}
