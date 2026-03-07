using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IDataResult<List<GetAllUsersResponse>>>
    {
        /// <summary>
        /// Filtre: Sadece belirli ClientId'ye ait kullanıcılar (null = tümü)
        /// </summary>
        public int? ClientId { get; set; }

        /// <summary>
        /// Filtre: UserType (null = tümü)
        /// </summary>
        public int? UserType { get; set; }
    }
}
