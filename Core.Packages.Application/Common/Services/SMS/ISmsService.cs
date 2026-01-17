namespace MagicCarRepairAISupported.Application.Common.Services.SMS
{
    /// <summary>
    /// SMS gönderme servisi interface'i
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// SMS gönderir
        /// </summary>
        /// <param name="phoneNumber">Telefon numarası (örn: 5551234567)</param>
        /// <param name="message">Mesaj içeriği</param>
        /// <returns>Gönderim başarılı mı?</returns>
        Task<bool> SendSmsAsync(string phoneNumber, string message);

        /// <summary>
        /// Toplu SMS gönderir
        /// </summary>
        /// <param name="phoneNumbers">Telefon numaraları listesi</param>
        /// <param name="message">Mesaj içeriği</param>
        /// <returns>Başarılı gönderim sayısı</returns>
        Task<int> SendBulkSmsAsync(List<string> phoneNumbers, string message);
    }
}

