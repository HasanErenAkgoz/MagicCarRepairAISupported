using MagicCarRepairAISupported.Application.Features.ErrorMessages.Queries.GetErrorMessageByCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ErrorMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get localized error message by code
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <param name="language">Language code (optional, defaults to request language)</param>
        [HttpGet("{errorCode}")]
        public async Task<IActionResult> GetErrorMessage(string errorCode, [FromQuery] string? language = null)
        {
            var result = await _mediator.Send(new GetErrorMessageByCodeQuery 
            { 
                ErrorCode = errorCode,
                Language = language
            });
            
            return Ok(result);
        }
    }
}

