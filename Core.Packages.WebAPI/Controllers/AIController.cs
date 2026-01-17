using MagicCarRepairAISupported.Application.Features.AI.Commands.Chat;
using MagicCarRepairAISupported.Application.Features.AI.Queries.OptimizeAppointments;
using MagicCarRepairAISupported.Application.Features.AI.Queries.ForecastStock;
using MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeCustomers;
using MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeEmployeePerformance;
using MagicCarRepairAISupported.Application.Features.AI.Queries.SuggestParts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// AI Chatbot ile konuş
        /// </summary>
        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli randevu optimizasyonu - En uygun randevu saatlerini öner
        /// </summary>
        [HttpGet("optimize-appointments")]
        public async Task<IActionResult> OptimizeAppointments([FromQuery] OptimizeAppointmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli stok tahmini - Parça stok tüketimini tahmin et
        /// </summary>
        [HttpGet("forecast-stock")]
        public async Task<IActionResult> ForecastStock([FromQuery] ForecastStockQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli müşteri analizi - Müşteri davranışını ve değerini analiz et
        /// </summary>
        [HttpGet("analyze-customers")]
        public async Task<IActionResult> AnalyzeCustomers([FromQuery] AnalyzeCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli personel performans analizi - Personel performansını analiz et
        /// </summary>
        [HttpGet("analyze-employee-performance")]
        public async Task<IActionResult> AnalyzeEmployeePerformance([FromQuery] AnalyzeEmployeePerformanceQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli parça önerisi - İş emri veya araç bilgisine göre parça öner
        /// </summary>
        [HttpGet("suggest-parts")]
        public async Task<IActionResult> SuggestParts([FromQuery] SuggestPartsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
