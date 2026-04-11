using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicShopList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    /// <summary>
    /// Public Clients Controller - Public tamirhane listesi endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/public-clients")]
    [AllowAnonymous]
    public class PublicClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublicClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Public tamirhane listesini getirir (onaylı / aktif tamirhaneler)
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> GetPublicShopList(
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? city = null)
        {
            var query = new GetPublicShopListQuery
            {
                SearchTerm = searchTerm,
                City = city
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
