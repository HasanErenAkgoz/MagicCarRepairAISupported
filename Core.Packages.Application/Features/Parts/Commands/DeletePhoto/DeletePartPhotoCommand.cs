using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePhoto
{
    public class DeletePartPhotoCommand : IRequest<IResult>
    {
        public int PartId { get; set; }
        public int PhotoId { get; set; }
    }
}
