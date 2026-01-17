using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart
{
    public class UpdatePartCommandValidator : AbstractValidator<UpdatePartCommand>
    {
        public UpdatePartCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Part ID must be greater than 0");

            RuleFor(x => x.PartCode)
                .NotEmpty().WithMessage("Part code is required")
                .MaximumLength(50).WithMessage("Part code must not exceed 50 characters");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Part name is required")
                .MaximumLength(200).WithMessage("Part name must not exceed 200 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.PurchasePrice)
                .GreaterThanOrEqualTo(0).WithMessage("Purchase price must be greater than or equal to 0");

            RuleFor(x => x.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("Sale price must be greater than or equal to 0");

            RuleFor(x => x.TaxRate)
                .InclusiveBetween(0, 100).WithMessage("Tax rate must be between 0 and 100");

            RuleFor(x => x.MinimumStockLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum stock level must be greater than or equal to 0");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Unit is required")
                .MaximumLength(20).WithMessage("Unit must not exceed 20 characters");

            RuleFor(x => x.Brand)
                .MaximumLength(100).WithMessage("Brand must not exceed 100 characters");

            RuleFor(x => x.OEMNumber)
                .MaximumLength(50).WithMessage("OEM number must not exceed 50 characters");

            RuleFor(x => x.Barcode)
                .MaximumLength(100).WithMessage("Barcode must not exceed 100 characters");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters");

            RuleFor(x => x.WarrantyMonths)
                .GreaterThanOrEqualTo(0).When(x => x.WarrantyMonths.HasValue)
                .WithMessage("Warranty months must be greater than or equal to 0");
        }
    }
}

