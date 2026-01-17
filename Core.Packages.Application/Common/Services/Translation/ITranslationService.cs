namespace MagicCarRepairAISupported.Application.Common.Services.Translation
{
    /// <summary>
    /// Çoklu dil desteği için translation servisi
    /// </summary>
    public interface ITranslationService
    {
        /// <summary>
        /// Belirtilen key ve dil için çeviri metnini getirir
        /// </summary>
        /// <param name="key">Çeviri anahtarı</param>
        /// <param name="language">Dil kodu (tr, en, de vb.)</param>
        /// <param name="defaultValue">Çeviri bulunamazsa döndürülecek varsayılan değer</param>
        /// <returns>Çevrilmiş metin</returns>
        Task<string> GetTranslationAsync(string key, string language = "tr", string? defaultValue = null);

        /// <summary>
        /// Mevcut HTTP context'ten dil bilgisini alır ve çeviri getirir
        /// </summary>
        /// <param name="key">Çeviri anahtarı</param>
        /// <param name="defaultValue">Çeviri bulunamazsa döndürülecek varsayılan değer</param>
        /// <returns>Çevrilmiş metin</returns>
        Task<string> GetTranslationAsync(string key, string? defaultValue = null);

        /// <summary>
        /// DomainException için özel çeviri metni getirir
        /// </summary>
        /// <param name="errorCode">Hata kodu</param>
        /// <param name="parameters">Mesaj parametreleri</param>
        /// <param name="language">Dil kodu</param>
        /// <returns>Çevrilmiş hata mesajı</returns>
        Task<string> GetDomainExceptionMessageAsync(string errorCode, object? parameters = null, string language = "tr");

        /// <summary>
        /// Desteklenen dilleri getirir
        /// </summary>
        /// <returns>Dil kodları listesi</returns>
        Task<List<string>> GetSupportedLanguagesAsync();

        /// <summary>
        /// Belirtilen dilde tüm çevirileri getirir
        /// </summary>
        /// <param name="language">Dil kodu</param>
        /// <returns>Çeviri sözlüğü</returns>
        Task<Dictionary<string, string>> GetAllTranslationsAsync(string language = "tr");
    }
}
