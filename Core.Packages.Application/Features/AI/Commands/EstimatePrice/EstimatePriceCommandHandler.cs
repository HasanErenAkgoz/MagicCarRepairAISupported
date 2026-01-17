using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.EstimatePrice
{
    public class EstimatePriceCommandHandler : IRequestHandler<EstimatePriceCommand, IDataResult<PriceEstimationResultDto>>
    {
        private readonly IAIPriceEstimationService _aiPriceEstimationService;

        public EstimatePriceCommandHandler(IAIPriceEstimationService aiPriceEstimationService)
        {
            _aiPriceEstimationService = aiPriceEstimationService;
        }

        public async Task<IDataResult<PriceEstimationResultDto>> Handle(EstimatePriceCommand request, CancellationToken cancellationToken)
        {
            var estimationRequest = new PriceEstimationRequestDto
            {
                WorkOrderId = request.WorkOrderId,
                CustomerId = request.CustomerId,
                VehicleId = request.VehicleId,
                ProblemDescription = request.ProblemDescription,
                PartIds = request.PartIds,
                LaborTypes = request.LaborTypes
            };

            var result = await _aiPriceEstimationService.EstimatePriceAsync(estimationRequest, cancellationToken);

            return new SuccessDataResult<PriceEstimationResultDto>(result, "Fiyat tahmini başarıyla tamamlandı");
        }
    }
}

