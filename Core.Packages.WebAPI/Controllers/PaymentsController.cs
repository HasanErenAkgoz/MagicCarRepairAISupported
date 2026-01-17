using MagicCarRepairAISupported.Application.Features.Payments.Commands.Callback;
using MagicCarRepairAISupported.Application.Features.Payments.Commands.Initialize;
using MagicCarRepairAISupported.Application.Features.Payments.Commands.Refund;
using MagicCarRepairAISupported.Application.Features.Payments.Queries.GetInstallmentOptions;
using MagicCarRepairAISupported.Application.Features.Payments.Queries.GetPaymentHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Ödeme başlat
        /// </summary>
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializePayment([FromBody] InitializePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Ödeme callback (3D Secure sonrası)
        /// </summary>
        [HttpPost("callback")]
        [AllowAnonymous] // İyzico'dan gelen callback için
        public async Task<IActionResult> HandleCallback([FromBody] HandlePaymentCallbackCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Ödeme iade
        /// </summary>
        [HttpPost("refund")]
        public async Task<IActionResult> RefundPayment([FromBody] RefundPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Taksit seçeneklerini getir
        /// </summary>
        [HttpGet("installment-options")]
        public async Task<IActionResult> GetInstallmentOptions([FromQuery] decimal amount)
        {
            var query = new GetInstallmentOptionsQuery { Amount = amount };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Ödeme geçmişini getir
        /// </summary>
        [HttpGet("history")]
        public async Task<IActionResult> GetPaymentHistory([FromQuery] GetPaymentHistoryQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}





