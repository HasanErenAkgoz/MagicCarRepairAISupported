namespace MagicCarRepairAISupported.Application.Common.Services.WhatsApp
{
    /// <summary>
    /// WhatsApp Business API servisi interface'i
    /// </summary>
    public interface IWhatsAppService
    {
        /// <summary>
        /// WhatsApp mesajı gönderir
        /// </summary>
        /// <param name="phoneNumber">Telefon numarası (örn: 905551234567)</param>
        /// <param name="message">Mesaj içeriği</param>
        /// <returns>Gönderim başarılı mı?</returns>
        Task<bool> SendMessageAsync(string phoneNumber, string message);

        /// <summary>
        /// WhatsApp medya (fotoğraf, video, doküman) gönderir
        /// </summary>
        /// <param name="phoneNumber">Telefon numarası</param>
        /// <param name="mediaUrl">Medya URL'i</param>
        /// <param name="mediaType">Medya tipi (image, video, document)</param>
        /// <param name="caption">Açıklama (opsiyonel)</param>
        /// <returns>Gönderim başarılı mı?</returns>
        Task<bool> SendMediaAsync(string phoneNumber, string mediaUrl, string mediaType, string? caption = null);

        /// <summary>
        /// WhatsApp interaktif buton mesajı gönderir
        /// </summary>
        /// <param name="phoneNumber">Telefon numarası</param>
        /// <param name="message">Mesaj içeriği</param>
        /// <param name="buttons">Buton listesi (max 3)</param>
        /// <returns>Gönderim başarılı mı?</returns>
        Task<bool> SendInteractiveMessageAsync(string phoneNumber, string message, List<WhatsAppButton> buttons);
    }

    /// <summary>
    /// WhatsApp buton modeli
    /// </summary>
    public class WhatsAppButton
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}

