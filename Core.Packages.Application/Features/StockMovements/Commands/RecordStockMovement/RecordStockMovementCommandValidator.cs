using FluentValidation;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Commands.RecordStockMovement
{
    public class RecordStockMovementCommandValidator : AbstractValidator<RecordStockMovementCommand>
    {
        public RecordStockMovementCommandValidator()
        {
            RuleFor(x => x.PartId)
                .GreaterThan(0).WithMessage("Part ID must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.ReferenceNumber)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.ReferenceNumber))
                .WithMessage("Reference number must not exceed 100 characters");

            RuleFor(x => x.ReferenceType)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.ReferenceType))
                .WithMessage("Reference type must not exceed 50 characters");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).When(x => x.UnitPrice.HasValue)
                .WithMessage("Unit price must be greater than or equal to 0");
        }
    }
}

