using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdateProfile
{
    public class UpdateClientProfileCommandValidator : AbstractValidator<UpdateClientProfileCommand>
    {
        public UpdateClientProfileCommandValidator()
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name)
                    .MaximumLength(200)
                    .WithMessage("İşletme adı en fazla 200 karakter olabilir.");
            });

            When(x => x.ContactEmail != null, () =>
            {
                RuleFor(x => x.ContactEmail)
                    .EmailAddress()
                    .WithMessage("Geçerli bir e-posta adresi giriniz.");
            });

            When(x => x.WebsiteUrl != null, () =>
            {
                RuleFor(x => x.WebsiteUrl)
                    .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                    .WithMessage("Geçerli bir web sitesi URL'si giriniz.");
            });
        }
    }
}
