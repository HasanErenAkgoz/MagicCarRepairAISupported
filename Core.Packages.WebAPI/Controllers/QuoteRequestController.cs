using MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetMyQuoteRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuoteRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Fiyat teklifi isteği oluştur
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateQuoteRequest([FromBody] CreateQuoteRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Müşterinin fiyat teklifi isteklerini getir
        /// </summary>
        [HttpGet("my-requests")]
        [Authorize]
        public async Task<IActionResult> GetMyQuoteRequests([FromQuery] int customerId)
        {
            var query = new GetMyQuoteRequestsQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Fiyat teklifi isteği detayını getir
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetQuoteRequestById(int id)
        {
            var query = new GetQuoteRequestByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
