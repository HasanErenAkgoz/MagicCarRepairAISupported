using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Features.Auth.Register.Commands;
using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.Permission.Commands.Create
{
    public class CreatePermissionValidation : AbstractValidator<RegisterCommand>
    {
        public CreatePermissionValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(Messages.NotEmpty);

        }
    }
}
