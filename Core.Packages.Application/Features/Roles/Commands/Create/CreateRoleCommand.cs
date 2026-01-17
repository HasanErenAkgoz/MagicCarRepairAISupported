using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommand : IRequest<IDataResult<int>>
    {
        public string Name { get; set; }
    }
}
