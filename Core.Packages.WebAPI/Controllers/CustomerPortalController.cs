using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.AddMyVehicle;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.AddWorkOrderPhoto;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.SubmitWorkOrderReview;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMaintenanceHistory;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyProfile;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyVehicles;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrderDetails;
using MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrders;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetMyQuoteRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MagicCarRepairAISupported.WebAPI.Authorization;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/customer-portal")]
    [Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
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
        /// Müşterinin profil bilgilerini getirir
        /// </summary>
        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
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

        /// <summary>
        /// Müşterinin iş emrine fotoğraf ekler
        /// </summary>
        [HttpPost("work-orders/{workOrderId}/photos")]
        public async Task<IActionResult> AddWorkOrderPhoto(int workOrderId, [FromBody] AddWorkOrderPhotoCommand command)
        {
            command.WorkOrderId = workOrderId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin kendi aracını ekler
        /// </summary>
        [HttpPost("vehicles")]
        public async Task<IActionResult> AddMyVehicle([FromBody] AddMyVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşteri iş emri değerlendirmesi gönderir
        /// </summary>
        [HttpPost("work-orders/{workOrderId}/review")]
        public async Task<IActionResult> SubmitWorkOrderReview(int workOrderId, [FromBody] SubmitWorkOrderReviewCommand command)
        {
            command.WorkOrderId = workOrderId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin fiyat teklifi isteklerini getirir
        /// </summary>
        [HttpGet("quote-requests")]
        public async Task<IActionResult> GetMyQuoteRequests()
        {
            try
            {
                // Get customer ID from profile
                var profileResult = await _mediator.Send(new GetMyProfileQuery());
                // GetMyProfileQuery returns UpdateMyProfileResponse directly (not wrapped in IDataResult)
                if (profileResult == null)
                {
                    return BadRequest(new { message = "Customer profile not found" });
                }

                // UpdateMyProfileResponse.Id is the CustomerId
                var customerId = profileResult.Id;
                var query = new GetMyQuoteRequestsQuery { CustomerId = customerId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}






