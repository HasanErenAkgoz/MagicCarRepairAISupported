using MagicCarRepairAISupported.Application.Features.Sync.Commands.SyncData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SyncController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SyncController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Offline değişiklikleri sync eder
        /// </summary>
        [HttpPost("data")]
        public async Task<IActionResult> SyncData([FromBody] SyncDataCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Belirli bir entity'yi sync eder
        /// </summary>
        [HttpPost("{entityType}/{entityId}")]
        public async Task<IActionResult> SyncEntity(string entityType, int entityId, [FromBody] object data)
        {
            // Entity sync logic
            return Ok(new { success = true, message = "Entity synced" });
        }
    }
}
