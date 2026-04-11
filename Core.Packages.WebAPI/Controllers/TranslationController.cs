using MagicCarRepairAISupported.Application.Common.Services.Translation;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserRepository _userRepository;

        public TranslationController(
            ITranslationService translationService,
            UserManager<UserEntity> userManager,
            IUserRepository userRepository)
        {
            _translationService = translationService;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Belirtilen key için çeviri getirir
        /// </summary>
        /// <param name="key">Çeviri anahtarı</param>
        /// <param name="language">Dil kodu (tr, en, de vb.)</param>
        /// <returns>Çevrilmiş metin</returns>
        [HttpGet("{key}")]
        public async Task<IActionResult> GetTranslation(string key, [FromQuery] string language = "tr")
        {
            var translation = await _translationService.GetTranslationAsync(key, language);
            return Ok(new { Key = key, Language = language, Value = translation });
        }

        /// <summary>
        /// Desteklenen dilleri getirir
        /// </summary>
        /// <returns>Dil kodları listesi</returns>
        [HttpGet("languages")]
        [AllowAnonymous] // Public endpoint - login öncesi dil seçici için gerekli
        public async Task<IActionResult> GetSupportedLanguages()
        {
            var languages = await _translationService.GetSupportedLanguagesAsync();
            return Ok(languages);
        }

        /// <summary>
        /// Belirtilen dilde tüm çevirileri getirir
        /// </summary>
        /// <param name="language">Dil kodu</param>
        /// <returns>Çeviri sözlüğü</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTranslations([FromQuery] string language = "tr")
        {
            var translations = await _translationService.GetAllTranslationsAsync(language);
            return Ok(translations);
        }

        /// <summary>
        /// Dil değiştirir (cookie olarak kaydeder ve User.Language DB'ye kaydeder)
        /// </summary>
        /// <param name="language">Dil kodu</param>
        /// <returns>Başarı mesajı</returns>
        [HttpPost("change-language")]
        public async Task<IActionResult> ChangeLanguage([FromBody] string language)
        {
            var supportedLanguages = await _translationService.GetSupportedLanguagesAsync();
            
            if (!supportedLanguages.Contains(language))
            {
                return BadRequest(new { Message = $"Desteklenmeyen dil: {language}" });
            }

            // Cookie'ye kaydet (web için)
            Response.Cookies.Append("language", language, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            // User.Language'ı DB'ye kaydet (mobile için kalıcılık)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var userId))
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user != null)
                {
                    user.Language = language;
                    await _userManager.UpdateAsync(user);
                }
            }

            return Ok(new { Message = $"Dil {language} olarak değiştirildi" });
        }
    }
}
