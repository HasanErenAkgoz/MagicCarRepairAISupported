using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest<IResult>
    {
        public int UserId { get; set; }

        /// <summary>
        /// 1=SystemAdmin, 2=Manager, 3=Employee, 4=Customer
        /// </summary>
        public int UserType { get; set; }
    }
}
