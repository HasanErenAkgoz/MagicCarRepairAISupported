using MagicCarRepairAISupported.Application.Features.Payments.Commands.InitializeMobilePayment;
using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MagicCarRepairAISupported.WebAPI.Authorization;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/mobile-payments")]
    [Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
    public class MobilePaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMobilePaymentService _mobilePaymentService;

        public MobilePaymentsController(IMediator mediator, IMobilePaymentService mobilePaymentService)
        {
            _mediator = mediator;
            _mobilePaymentService = mobilePaymentService;
        }

        /// <summary>
        /// Mobil ödeme oturumu başlatır
        /// </summary>
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializePayment([FromBody] InitializeMobilePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Ödeme sonucunu doğrular
        /// </summary>
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyPayment([FromBody] VerifyPaymentRequest request)
        {
            var result = await _mobilePaymentService.VerifyPaymentAsync(request.ConversationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Taksit seçeneklerini getirir
        /// </summary>
        [HttpGet("installments")]
        public async Task<IActionResult> GetInstallments([FromQuery] decimal amount, [FromQuery] string binNumber)
        {
            var options = await _mobilePaymentService.GetInstallmentOptionsAsync(amount, binNumber);
            return Ok(options);
        }
    }

    public class VerifyPaymentRequest
    {
        public string ConversationId { get; set; } = string.Empty;
    }
}
