using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Commands.DeactivateReward
{
    public class DeactivateRewardCommand : IRequest<IDataResult<DeactivateRewardResponse>>
    {
        public int Id { get; set; }
    }

    public class DeactivateRewardResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
