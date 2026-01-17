using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetFacilityPhoto
{
    public class GetFacilityPhotoQuery : IRequest<GetFacilityPhotoResponse>
    {
        public int Id { get; set; }
    }
}
