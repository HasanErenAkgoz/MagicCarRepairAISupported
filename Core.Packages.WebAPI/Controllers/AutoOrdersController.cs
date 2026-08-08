using MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Approve;
using MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Create;
using MagicCarRepairAISupported.Application.Features.AutoOrders.Queries.GetPending;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class AutoOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutoOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Bekleyen otomatik siparişleri getirir
        /// </summary>
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingOrders()
        {
            var query = new GetPendingAutoOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// StockAlert'ten otomatik sipariş oluşturur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAutoOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetPendingOrders), result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Otomatik siparişi onaylar
        /// </summary>
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApproveAutoOrderCommand command)
        {
            command.AutoOrderId = id;
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

