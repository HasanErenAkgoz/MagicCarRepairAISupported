using MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.ReplyToRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Commands.ModerateRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByClient;
using MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetAverageRating;
using MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByWorkOrder;
using MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingStatistics;
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
    public class RatingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Değerlendirme oluştur (Müşteri için)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] CreateRatingCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateRating), new { id = result.Id }, result);
        }

        /// <summary>
        /// Servisin tüm değerlendirmelerini getir
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetRatingsByClient([FromQuery] GetRatingsByClientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Ortalama puan ve istatistikleri getir
        /// </summary>
        [HttpGet("average")]
        public async Task<IActionResult> GetAverageRating()
        {
            var query = new GetAverageRatingQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Değerlendirmeye servis yanıtı ver
        /// </summary>
        [HttpPost("{id}/reply")]
        public async Task<IActionResult> ReplyToRating(int id, [FromBody] ReplyToRatingCommand command)
        {
            command.RatingId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Değerlendirmeyi onayla/reddet (Admin)
        /// </summary>
        [HttpPut("{id}/moderate")]
        public async Task<IActionResult> ModerateRating(int id, [FromBody] ModerateRatingCommand command)
        {
            command.RatingId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrine ait değerlendirmeleri getir
        /// </summary>
        [HttpGet("workorder/{workOrderId}")]
        public async Task<IActionResult> GetRatingsByWorkOrder(int workOrderId)
        {
            var query = new GetRatingsByWorkOrderQuery { WorkOrderId = workOrderId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Değerlendirme istatistiklerini getir
        /// </summary>
        [HttpGet("statistics")]
        public async Task<IActionResult> GetRatingStatistics([FromQuery] GetRatingStatisticsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
