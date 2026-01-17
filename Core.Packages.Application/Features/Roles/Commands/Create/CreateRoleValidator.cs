using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Features.Auth.Register.Commands;
using FluentValidation;

namespace MagicCarRepairAISupported.Application.Features.Roles.Commands.Create
{
    public class CreateRoleValidator : AbstractValidator<RegisterCommand>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(Messages.NotEmpty);
        }
    }
}
