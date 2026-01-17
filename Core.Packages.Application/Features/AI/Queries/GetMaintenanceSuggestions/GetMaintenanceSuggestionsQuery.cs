using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.GetMaintenanceSuggestions
{
    public class GetMaintenanceSuggestionsQuery : IRequest<IDataResult<List<MaintenanceSuggestionDto>>>
    {
        public int VehicleId { get; set; }
    }
}

