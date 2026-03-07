using MagicCarRepairAISupported.Application.Features.QRCode.Commands.GenerateQRCode;
using MagicCarRepairAISupported.Application.Features.QRCode.Commands.ScanQRCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/qr-code")]
    [Authorize]
    public class QRCodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QRCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// QR kod oluşturur
        /// </summary>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateQRCode([FromBody] GenerateQRCodeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// QR kod okur ve doğrular
        /// </summary>
        [HttpPost("scan")]
        public async Task<IActionResult> ScanQRCode([FromBody] ScanQRCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
