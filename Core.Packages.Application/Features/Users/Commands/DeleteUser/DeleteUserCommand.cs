using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
    }
}
