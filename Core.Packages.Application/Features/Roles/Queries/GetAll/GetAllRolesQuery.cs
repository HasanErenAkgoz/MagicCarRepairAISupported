using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesQuery : IRequest<IDataResult<List<GetAllRolesResponse>>>
    {
        /// <summary>
        /// Belirli bir client'ın rollerini getirmek için (SystemAdmin kullanımı).
        /// Null ise tenant servisinden alınır.
        /// </summary>
        public int? ClientId { get; set; }
    }
}
