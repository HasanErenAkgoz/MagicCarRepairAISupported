using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.EstimatePrice
{
    public class EstimatePriceCommand : IRequest<IDataResult<PriceEstimationResultDto>>
    {
        public int WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public string? ProblemDescription { get; set; }
        public List<int>? PartIds { get; set; }
        public List<string>? LaborTypes { get; set; }
    }
}

