using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.UseRoles.Commands.Create
{
    public class CreateUserRoleCommand : IRequest<IResult>
    {
        public UserEntity User { get; set; }
        public string RoleName { get; set; }
    }
}
