using MagicCarRepairAISupported.Application.Features.Help.Queries.GetHelpArticles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HelpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HelpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yardım makalelerini getirir
        /// </summary>
        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles([FromQuery] string? category = null)
        {
            var query = new GetHelpArticlesQuery { Category = category };
            var result = await _mediator.Send(query);
            
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
