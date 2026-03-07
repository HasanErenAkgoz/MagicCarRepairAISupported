using MagicCarRepairAISupported.Application.Features.Onboarding.Commands.CompleteOnboardingStep;
using MagicCarRepairAISupported.Application.Features.Onboarding.Queries.GetOnboardingStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OnboardingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OnboardingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Kullanıcının onboarding durumunu getirir
        /// </summary>
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var query = new GetOnboardingStatusQuery { UserId = userId };
            var result = await _mediator.Send(query);
            
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Onboarding adımını tamamlar
        /// </summary>
        [HttpPost("complete-step")]
        public async Task<IActionResult> CompleteStep([FromBody] CompleteOnboardingStepRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var command = new CompleteOnboardingStepCommand
            {
                UserId = userId,
                Step = request.Step
            };
            
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }

    public class CompleteOnboardingStepRequest
    {
        public string Step { get; set; } = string.Empty;
    }
}
