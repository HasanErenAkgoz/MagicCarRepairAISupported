using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.UseRoles.Commands.Create
{
    public class CreateUserRoleCommand : IRequest<IResult>
    {
        public User User { get; set; }
        public string RoleName { get; set; }
    }
}
