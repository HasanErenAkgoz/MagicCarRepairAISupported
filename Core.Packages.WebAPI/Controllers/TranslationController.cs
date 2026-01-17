using MagicCarRepairAISupported.Application.Common.Services.Translation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;

        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
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
        /// Dil değiştirir (cookie olarak kaydeder)
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

            Response.Cookies.Append("language", language, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { Message = $"Dil {language} olarak değiştirildi" });
        }
    }
}
