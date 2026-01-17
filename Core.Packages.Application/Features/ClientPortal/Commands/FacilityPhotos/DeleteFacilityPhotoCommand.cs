using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.FacilityPhotos
{
    public class DeleteFacilityPhotoCommand : IRequest<DeleteFacilityPhotoResponse>
    {
        public int Id { get; set; }
    }
}
