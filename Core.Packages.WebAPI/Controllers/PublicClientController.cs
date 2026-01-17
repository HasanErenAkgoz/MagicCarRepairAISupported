using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicCertificates;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicFacilities;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicPortfolio;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicProfile;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicReviews;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicStatistics;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicTeam;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    /// <summary>
    /// Public Client Profile Controller - Herkese açık tamirhane profili ve portföy endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/public/clients")]
    [AllowAnonymous]
    public class PublicClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PublicClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Public profil bilgilerini getirir
        /// </summary>
        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> GetPublicProfile(int clientId)
        {
            var query = new GetPublicProfileQuery { ClientId = clientId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Client Code ile public profil bilgilerini getirir
        /// </summary>
        [HttpGet("code/{clientCode}")]
        public async Task<IActionResult> GetPublicProfileByCode(string clientCode)
        {
            var query = new GetPublicProfileQuery { ClientCode = clientCode };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Public portföy listesini getirir
        /// </summary>
        [HttpGet("{clientId:int}/portfolio")]
        public async Task<IActionResult> GetPublicPortfolio(
            int clientId,
            [FromQuery] string? category = null,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null)
        {
            var query = new GetPublicPortfolioQuery
            {
                ClientId = clientId,
                Category = category,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Public sertifika listesini getirir
        /// </summary>
        [HttpGet("{clientId:int}/certificates")]
        public async Task<IActionResult> GetPublicCertificates(int clientId)
        {
            var query = new GetPublicCertificatesQuery { ClientId = clientId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Public ekip listesini getirir
        /// </summary>
        [HttpGet("{clientId:int}/team")]
        public async Task<IActionResult> GetPublicTeam(int clientId)
        {
            var query = new GetPublicTeamQuery { ClientId = clientId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Public tesis fotoğraflarını getirir
        /// </summary>
        [HttpGet("{clientId:int}/facilities")]
        public async Task<IActionResult> GetPublicFacilities(
            int clientId,
            [FromQuery] string? category = null)
        {
            var query = new GetPublicFacilitiesQuery
            {
                ClientId = clientId,
                Category = category
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Public istatistikleri getirir
        /// </summary>
        [HttpGet("{clientId:int}/statistics")]
        public async Task<IActionResult> GetPublicStatistics(int clientId)
        {
            var query = new GetPublicStatisticsQuery { ClientId = clientId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Public değerlendirmeleri (reviews) getirir
        /// </summary>
        [HttpGet("{clientId:int}/reviews")]
        public async Task<IActionResult> GetPublicReviews(
            int clientId,
            [FromQuery] int? pageNumber = null,
            [FromQuery] int? pageSize = null)
        {
            var query = new GetPublicReviewsQuery
            {
                ClientId = clientId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
