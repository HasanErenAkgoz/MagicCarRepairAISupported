using MagicCarRepairAISupported.Application.Features.StockAlerts.Commands.Resolve;
using MagicCarRepairAISupported.Application.Features.StockAlerts.Queries.GetActive;
using MagicCarRepairAISupported.Domain.Enums;
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
    public class StockAlertsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockAlertsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Aktif stok alarmlarını getirir
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAlerts(
            [FromQuery] StockAlertType? alertType,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new GetActiveStockAlertsQuery
            {
                AlertType = alertType,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Stok alarmını çözüldü olarak işaretler
        /// </summary>
        [HttpPost("{id}/resolve")]
        public async Task<IActionResult> ResolveAlert(int id)
        {
            var command = new ResolveStockAlertCommand { StockAlertId = id };
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

