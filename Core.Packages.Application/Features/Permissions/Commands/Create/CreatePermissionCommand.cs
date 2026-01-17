using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Permission.Commands.Create
{
    public class CreatePermissionCommand : IRequest<IResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
