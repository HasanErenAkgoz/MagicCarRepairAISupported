using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.EstimatePartLifespan
{
    public class EstimatePartLifespanQueryHandler : IRequestHandler<EstimatePartLifespanQuery, IDataResult<PartLifespanEstimateDto>>
    {
        private readonly IAIMaintenanceService _aiMaintenanceService;

        public EstimatePartLifespanQueryHandler(IAIMaintenanceService aiMaintenanceService)
        {
            _aiMaintenanceService = aiMaintenanceService;
        }

        public async Task<IDataResult<PartLifespanEstimateDto>> Handle(EstimatePartLifespanQuery request, CancellationToken cancellationToken)
        {
            var result = await _aiMaintenanceService.EstimatePartLifespanAsync(request.PartId, request.VehicleId, cancellationToken);

            return new SuccessDataResult<PartLifespanEstimateDto>(result, "Parça ömrü tahmini başarıyla tamamlandı");
        }
    }
}

