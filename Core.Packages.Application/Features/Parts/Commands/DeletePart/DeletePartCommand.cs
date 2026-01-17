using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePart
{
    public class DeletePartCommand : IRequest<DeletePartResponse>
    {
        public int Id { get; set; }
    }
}

