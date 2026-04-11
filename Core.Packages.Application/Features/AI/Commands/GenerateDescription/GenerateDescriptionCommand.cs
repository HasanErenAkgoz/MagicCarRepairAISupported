using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.GenerateDescription
{
    public class GenerateDescriptionCommand : IRequest<IDataResult<GenerateDescriptionResultDto>>
    {
        public string ShopName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
