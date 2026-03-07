using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzeDamagePhotos
{
    public class AnalyzeDamagePhotosCommand : IRequest<IDataResult<AnalyzeDamagePhotosResponse>>
    {
        public List<string> PhotoPaths { get; set; } = new List<string>();
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }
        public string? ProblemDescription { get; set; }
    }

    public class AnalyzeDamagePhotosResponse
    {
        public decimal EstimatedCost { get; set; }
        public string DamageDescription { get; set; } = string.Empty;
        public List<string> PartsNeeded { get; set; } = new List<string>();
        public string DetailedAnalysis { get; set; } = string.Empty;
        public int ConfidenceScore { get; set; } // 0-100
    }
}
