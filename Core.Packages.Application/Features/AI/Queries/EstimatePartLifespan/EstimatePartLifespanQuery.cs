using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.EstimatePartLifespan
{
    public class EstimatePartLifespanQuery : IRequest<IDataResult<PartLifespanEstimateDto>>
    {
        public int PartId { get; set; }
        public int VehicleId { get; set; }
    }
}

