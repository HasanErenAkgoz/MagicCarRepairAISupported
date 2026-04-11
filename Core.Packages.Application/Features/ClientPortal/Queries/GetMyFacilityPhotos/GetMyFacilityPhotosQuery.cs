using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyProfile;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyFacilityPhotos
{
    public class GetMyFacilityPhotosQuery : IRequest<List<FacilityPhotoDto>>
    {
    }
}
