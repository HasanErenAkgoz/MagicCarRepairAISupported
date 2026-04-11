using MagicCarRepairAISupported.Application.Features.Rewards.Commands.CreateReward;
using MagicCarRepairAISupported.Application.Features.Rewards.Commands.DeactivateReward;
using MagicCarRepairAISupported.Application.Features.Rewards.Commands.UpdateReward;
using MagicCarRepairAISupported.Application.Features.Rewards.Queries.GetRewards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RewardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RewardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yeni ödül oluşturur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRewardCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Tüm ödülleri getirir
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetRewardsQuery { ActiveOnly = false };
            var result = await _mediator.Send(query);
            // Mobile expects { success, data: RewardDto[] } — return flat array
            return Ok(new { success = result.Success, data = result.Data?.Rewards, message = result.Message });
        }

        /// <summary>
        /// Aktif ödülleri getirir
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var query = new GetRewardsQuery { ActiveOnly = true };
            var result = await _mediator.Send(query);
            // Mobile expects { success, data: RewardDto[] } — return flat array
            return Ok(new { success = result.Success, data = result.Data?.Rewards, message = result.Message });
        }

        /// <summary>
        /// Ödülü günceller
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRewardCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Ödülü pasife alır
        /// </summary>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var command = new DeactivateRewardCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
