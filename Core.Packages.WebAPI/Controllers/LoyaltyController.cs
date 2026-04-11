using MagicCarRepairAISupported.Application.Features.Loyalty.Commands.EarnPoints;
using MagicCarRepairAISupported.Application.Features.Loyalty.Commands.RedeemPoints;
using MagicCarRepairAISupported.Application.Features.Loyalty.Queries.GetCustomerPoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoyaltyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoyaltyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Müşteriye puan ekler
        /// </summary>
        [HttpPost("earn")]
        public async Task<IActionResult> EarnPoints([FromBody] EarnPointsCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Puan kullanarak ödül talep eder
        /// </summary>
        [HttpPost("redeem")]
        public async Task<IActionResult> RedeemPoints([FromBody] RedeemPointsCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Müşterinin puan bilgilerini getirir
        /// </summary>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerPoints(int customerId)
        {
            var query = new GetCustomerPointsQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
