using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100);

            RuleFor(x => x.IdentityNo)
                .MaximumLength(11).When(x => !string.IsNullOrWhiteSpace(x.IdentityNo));

            RuleFor(x => x.Email)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
        }
    }
}
