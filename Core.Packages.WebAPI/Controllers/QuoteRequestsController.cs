using MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Accept;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Reject;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetOpen;
using MagicCarRepairAISupported.Application.Features.QuoteResponses.Commands.Submit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuoteRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuoteRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yeni teklif talebi oluşturur (Müşteri)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuoteRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Teklif talebi detayını getirir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetQuoteRequestByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        /// <summary>
        /// Tüm teklif taleplerini getirir (Filtreleme ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllQuoteRequestsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Açık teklif taleplerini getirir (Servisler için - teklif verebilecekleri)
        /// </summary>
        [HttpGet("open")]
        public async Task<IActionResult> GetOpen([FromQuery] GetOpenQuoteRequestsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Teklif talebine teklif verir (Servis)
        /// </summary>
        [HttpPost("{quoteRequestId}/quotes")]
        public async Task<IActionResult> SubmitQuote(int quoteRequestId, [FromBody] SubmitQuoteResponseCommand command)
        {
            command.QuoteRequestId = quoteRequestId;
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = quoteRequestId }, result.Data);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Teklifi kabul eder (Müşteri)
        /// </summary>
        [HttpPost("{quoteRequestId}/quotes/{quoteResponseId}/accept")]
        public async Task<IActionResult> AcceptQuote(int quoteRequestId, int quoteResponseId)
        {
            var command = new AcceptQuoteResponseCommand
            {
                QuoteRequestId = quoteRequestId,
                QuoteResponseId = quoteResponseId
            };
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Teklifi reddeder (Müşteri)
        /// </summary>
        [HttpPost("{quoteRequestId}/quotes/{quoteResponseId}/reject")]
        public async Task<IActionResult> RejectQuote(int quoteRequestId, int quoteResponseId, [FromBody] RejectQuoteResponseCommand? command = null)
        {
            if (command == null)
            {
                command = new RejectQuoteResponseCommand();
            }
            command.QuoteRequestId = quoteRequestId;
            command.QuoteResponseId = quoteResponseId;
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

