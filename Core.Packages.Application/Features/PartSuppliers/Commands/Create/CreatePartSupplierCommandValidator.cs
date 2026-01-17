using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Create
{
    public class CreatePartSupplierCommandValidator : AbstractValidator<CreatePartSupplierCommand>
    {
        public CreatePartSupplierCommandValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(200).WithMessage("Company name must not exceed 200 characters");

            RuleFor(x => x.ContactPerson)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.ContactPerson))
                .WithMessage("Contact person must not exceed 100 characters");

            RuleFor(x => x.Phone)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone must not exceed 20 characters");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email address")
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email must not exceed 100 characters");

            RuleFor(x => x.Address)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Address))
                .WithMessage("Address must not exceed 500 characters");

            RuleFor(x => x.City)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.City))
                .WithMessage("City must not exceed 50 characters");

            RuleFor(x => x.Country)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Country))
                .WithMessage("Country must not exceed 50 characters");

            RuleFor(x => x.TaxNumber)
                .MaximumLength(20).When(x => !string.IsNullOrEmpty(x.TaxNumber))
                .WithMessage("Tax number must not exceed 20 characters");

            RuleFor(x => x.TaxOffice)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.TaxOffice))
                .WithMessage("Tax office must not exceed 100 characters");

            RuleFor(x => x.PaymentTerms)
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.PaymentTerms))
                .WithMessage("Payment terms must not exceed 200 characters");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Notes))
                .WithMessage("Notes must not exceed 1000 characters");
        }
    }
}

