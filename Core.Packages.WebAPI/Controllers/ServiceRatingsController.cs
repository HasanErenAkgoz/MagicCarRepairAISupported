using MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByClient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/service-ratings")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class ServiceRatingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceRatingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Değerlendirme oluşturur (Müşteri için)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateRating), new { id = result.Id }, result);
        }

        /// <summary>
        /// Servise ait public değerlendirmeleri getirir
        /// </summary>
        [HttpGet("public/{clientId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicRatings(
            int clientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] int? minRating = null)
        {
            var query = new GetRatingsByClientQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                OnlyApproved = true,
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
