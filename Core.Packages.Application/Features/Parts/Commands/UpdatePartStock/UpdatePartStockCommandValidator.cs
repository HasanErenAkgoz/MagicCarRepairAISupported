using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePartStock
{
    public class UpdatePartStockCommandValidator : AbstractValidator<UpdatePartStockCommand>
    {
        public UpdatePartStockCommandValidator()
        {
            RuleFor(x => x.PartId)
                .GreaterThan(0).WithMessage("Part ID must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).When(x => x.Quantity.HasValue)
                .WithMessage("Quantity must be greater than or equal to 0");

            RuleFor(x => x.Location)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Location))
                .WithMessage("Location must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description must not exceed 500 characters");
        }
    }
}

