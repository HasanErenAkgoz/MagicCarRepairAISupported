using MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.AddPortfolio;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.DeletePortfolio;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdatePortfolio;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.Certificates;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.FacilityPhotos;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdateProfile;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetCertificate;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetFacilityPhoto;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyProfile;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyFacilityPhotos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    /// <summary>
    /// Client Portal Controller - Tamirhane yönetim endpoint'leri (Authorize gerekli)
    /// </summary>
    [ApiController]
    [Route("api/client-portal")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class ClientPortalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientPortalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Portföye yeni iş emri ekler
        /// </summary>
        [HttpPost("portfolio")]
        public async Task<IActionResult> AddPortfolio([FromBody] AddPortfolioCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Portföy öğesini günceller
        /// </summary>
        [HttpPut("portfolio/{id:int}")]
        public async Task<IActionResult> UpdatePortfolio(int id, [FromBody] UpdatePortfolioCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Portföy öğesini siler
        /// </summary>
        [HttpDelete("portfolio/{id:int}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var command = new DeletePortfolioCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Yeni sertifika ekler
        /// </summary>
        [HttpPost("certificates")]
        public async Task<IActionResult> CreateCertificate([FromBody] CreateCertificateCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCertificate), new { id = result.Id }, result);
        }

        /// <summary>
        /// Sertifikayı günceller
        /// </summary>
        [HttpPut("certificates/{id:int}")]
        public async Task<IActionResult> UpdateCertificate(int id, [FromBody] UpdateCertificateCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Sertifikayı siler
        /// </summary>
        [HttpDelete("certificates/{id:int}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            var command = new DeleteCertificateCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Sertifikayı ID'ye göre getirir
        /// </summary>
        [HttpGet("certificates/{id:int}")]
        public async Task<IActionResult> GetCertificate(int id)
        {
            var query = new GetCertificateQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni tesis fotoğrafı ekler
        /// </summary>
        [HttpPost("facility-photos")]
        public async Task<IActionResult> CreateFacilityPhoto([FromBody] CreateFacilityPhotoCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFacilityPhoto), new { id = result.Id }, result);
        }

        /// <summary>
        /// Tesis fotoğrafını günceller
        /// </summary>
        [HttpPut("facility-photos/{id:int}")]
        public async Task<IActionResult> UpdateFacilityPhoto(int id, [FromBody] UpdateFacilityPhotoCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Tesis fotoğrafını siler
        /// </summary>
        [HttpDelete("facility-photos/{id:int}")]
        public async Task<IActionResult> DeleteFacilityPhoto(int id)
        {
            var command = new DeleteFacilityPhotoCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Tesis fotoğrafını ID'ye göre getirir
        /// </summary>
        [HttpGet("facility-photos/{id:int}")]
        public async Task<IActionResult> GetFacilityPhoto(int id)
        {
            var query = new GetFacilityPhotoQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Mevcut müşterinin işletme profilini getirir
        /// </summary>
        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }

        /// <summary>
        /// Mevcut müşterinin işletme profilini günceller
        /// </summary>
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateClientProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Mevcut müşterinin tüm tesis fotoğraflarını listeler
        /// </summary>
        [HttpGet("my-facility-photos")]
        public async Task<IActionResult> GetMyFacilityPhotos()
        {
            var result = await _mediator.Send(new GetMyFacilityPhotosQuery());
            return Ok(result);
        }
    }
}
