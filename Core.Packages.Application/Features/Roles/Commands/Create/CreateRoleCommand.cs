using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommand : IRequest<IDataResult<int>>
    {
        public string Name { get; set; }

        /// <summary>
        /// SystemAdmin kullanımı için opsiyonel clientId override.
        /// Null ise tenant servisinden alınır.
        /// </summary>
        public int? ClientId { get; set; }
    }
}
