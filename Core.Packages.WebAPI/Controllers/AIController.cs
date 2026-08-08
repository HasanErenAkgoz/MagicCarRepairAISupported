using MagicCarRepairAISupported.Application.Features.AI.Commands.Chat;
using MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzeDamagePhotos;
using MagicCarRepairAISupported.Application.Features.AI.Commands.UploadDiagnosisAsset;
using MagicCarRepairAISupported.Application.Features.AI.Commands.Diagnose;
using MagicCarRepairAISupported.Application.Features.AI.Commands.GenerateDescription;
using MagicCarRepairAISupported.Application.Features.AI.Queries.OptimizeAppointments;
using MagicCarRepairAISupported.Application.Features.AI.Queries.ForecastStock;
using MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeCustomers;
using MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeEmployeePerformance;
using MagicCarRepairAISupported.Application.Features.AI.Queries.SuggestParts;
using MagicCarRepairAISupported.Application.Features.AI.Queries.ResolvePartPrices;
using MediatR;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public AIController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        /// <summary>
        /// AI Chatbot ile konuş
        /// </summary>
        [HttpPost("chat")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> Chat([FromBody] ChatCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli randevu optimizasyonu - En uygun randevu saatlerini öner
        /// </summary>
        [HttpGet("optimize-appointments")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> OptimizeAppointments([FromQuery] OptimizeAppointmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli stok tahmini - Parça stok tüketimini tahmin et
        /// </summary>
        [HttpGet("forecast-stock")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> ForecastStock([FromQuery] ForecastStockQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli müşteri analizi - Müşteri davranışını ve değerini analiz et
        /// </summary>
        [HttpGet("analyze-customers")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> AnalyzeCustomers([FromQuery] AnalyzeCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli personel performans analizi - Personel performansını analiz et
        /// </summary>
        [HttpGet("analyze-employee-performance")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> AnalyzeEmployeePerformance([FromQuery] AnalyzeEmployeePerformanceQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// AI destekli arıza teşhisi - Müşteri şikayetine göre olası arızaları, gerekli parça ve işçilikleri tahmin et
        /// </summary>
        [HttpPost("diagnose")]
        [Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
        public async Task<IActionResult> Diagnose([FromBody] DiagnoseCommand command)
        {
            // Read Accept-Language from request header; default to "tr"
            var acceptLang = Request.Headers["Accept-Language"].FirstOrDefault() ?? "tr";
            var primaryLang = acceptLang.Split(',')[0].Trim().ToLower();
            command.Language = primaryLang.StartsWith("en") ? "en" : "tr";

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("diagnosis-assets")]
        [Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadDiagnosisAsset([FromForm] UploadDiagnosisAssetCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// AI destekli parça önerisi - İş emri veya araç bilgisine göre parça öner
        /// </summary>
        [HttpGet("suggest-parts")]
        [Authorize(Policy = AuthPolicyNames.ShopStaff)]
        public async Task<IActionResult> SuggestParts([FromQuery] SuggestPartsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Servis yeri için AI destekli benzersiz açıklama üretir — kayıt akışı için anonim erişim açık
        /// </summary>
        [HttpPost("generate-description")]
        [AllowAnonymous]
        [EnableRateLimiting("ai-anonymous")]
        public async Task<IActionResult> GenerateDescription([FromBody] GenerateDescriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// AI teşhis sonrası çapraz servis envanterinde parça fiyatı çözümleme.
        /// Tüm aktif ve public-profile açık servisler taranır; fiyat + güven skoru döner.
        /// </summary>
        [HttpGet("resolve-part-prices")]
        [Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
        public async Task<IActionResult> ResolvePartPrices([FromQuery] ResolvePartPricesQuery query)
        {
            var result = await _mediator.Send(query);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// AI destekli hasar fotoğrafı analizi - Fotoğraflardan hasar tespiti ve maliyet tahmini
        /// </summary>
        [HttpPost("analyze-damage-photos")]
        [Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
        public async Task<IActionResult> AnalyzeDamagePhotos([FromBody] AnalyzeDamagePhotosCommand command)
        {
            // The legacy command accepts raw paths. Production must not dereference caller input.
            if (_environment.IsProduction())
                return StatusCode(StatusCodes.Status410Gone, new { message = "Damage photo analysis is disabled until authorized media asset IDs are available." });

            var acceptLang = Request.Headers["Accept-Language"].FirstOrDefault() ?? "tr";
            var primaryLang = acceptLang.Split(',')[0].Trim().ToLower();
            command.Language = primaryLang.StartsWith("en") ? "en" : "tr";

            var result = await _mediator.Send(command);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
