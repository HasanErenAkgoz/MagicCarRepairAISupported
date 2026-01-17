using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMaintenanceHistory;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyVehicles;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrderDetails;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/customer-portal")]
    [Authorize]
    public class CustomerPortalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPortalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Portföy öğesini onaylar
        /// </summary>
        [HttpPost("portfolio/{portfolioId:int}/approve")]
        public async Task<IActionResult> ApprovePortfolio(int portfolioId)
        {
            var command = new MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.ApprovePortfolio.ApprovePortfolioCommand
            {
                PortfolioId = portfolioId
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Portföy öğesini reddeder
        /// </summary>
        [HttpPost("portfolio/{portfolioId:int}/reject")]
        public async Task<IActionResult> RejectPortfolio(int portfolioId, [FromBody] MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.RejectPortfolio.RejectPortfolioCommand command)
        {
            command.PortfolioId = portfolioId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin kendi araçlarını getirir
        /// </summary>
        [HttpGet("vehicles")]
        public async Task<IActionResult> GetMyVehicles()
        {
            var result = await _mediator.Send(new GetMyVehiclesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin kendi iş emirlerini getirir
        /// </summary>
        [HttpGet("work-orders")]
        public async Task<IActionResult> GetMyWorkOrders([FromQuery] bool? activeOnly = null)
        {
            var result = await _mediator.Send(new GetMyWorkOrdersQuery { ActiveOnly = activeOnly });
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin iş emri detaylarını getirir
        /// </summary>
        [HttpGet("work-orders/{id}")]
        public async Task<IActionResult> GetMyWorkOrderDetails(int id)
        {
            var result = await _mediator.Send(new GetMyWorkOrderDetailsQuery { WorkOrderId = id });
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin bakım geçmişini getirir
        /// </summary>
        [HttpGet("maintenance-history")]
        public async Task<IActionResult> GetMaintenanceHistory([FromQuery] int? vehicleId = null)
        {
            var result = await _mediator.Send(new GetMaintenanceHistoryQuery { VehicleId = vehicleId });
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin profil bilgilerini günceller
        /// </summary>
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMyProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}






