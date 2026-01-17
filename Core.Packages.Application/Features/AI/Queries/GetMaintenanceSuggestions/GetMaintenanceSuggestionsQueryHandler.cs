using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.GetMaintenanceSuggestions
{
    public class GetMaintenanceSuggestionsQueryHandler : IRequestHandler<GetMaintenanceSuggestionsQuery, IDataResult<List<MaintenanceSuggestionDto>>>
    {
        private readonly IAIMaintenanceService _aiMaintenanceService;

        public GetMaintenanceSuggestionsQueryHandler(IAIMaintenanceService aiMaintenanceService)
        {
            _aiMaintenanceService = aiMaintenanceService;
        }

        public async Task<IDataResult<List<MaintenanceSuggestionDto>>> Handle(GetMaintenanceSuggestionsQuery request, CancellationToken cancellationToken)
        {
            var suggestions = await _aiMaintenanceService.GetMaintenanceSuggestionsAsync(request.VehicleId, cancellationToken);

            return new SuccessDataResult<List<MaintenanceSuggestionDto>>(suggestions, "Bakım önerileri başarıyla getirildi");
        }
    }
}

