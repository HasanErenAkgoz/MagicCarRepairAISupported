using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Attributies;
using MagicCarRepairAISupported.Application.Common.AutoMapper;
namespace MagicCarRepairAISupported.Application.Features.Permission.Queries.GetAll
{
    public sealed class GetPermissionResponse : IMapFrom<MagicCarRepairAISupported.Domain.Entities.Permission>
    {

        public GetPermissionResponse() { }  // ?? Default Constructor ekledik

        [Translate]
        public string Name { get; set; }
        [Translate]
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MagicCarRepairAISupported.Domain.Entities.Permission, GetPermissionResponse>();
        }
    }
}
