using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Kullanıcı cihaz token'ları (FCM push notifications için)
    /// </summary>
    public class UserDeviceToken : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// FCM Token
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Cihaz tipi (iOS, Android, Web)
        /// </summary>
        public string DeviceType { get; set; } = string.Empty;

        /// <summary>
        /// Cihaz modeli (opsiyonel)
        /// </summary>
        public string? DeviceModel { get; set; }

        /// <summary>
        /// Cihaz OS versiyonu (opsiyonel)
        /// </summary>
        public string? DeviceOSVersion { get; set; }

        /// <summary>
        /// Token aktif mi?
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Son kullanım tarihi
        /// </summary>
        public DateTime? LastUsedDate { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
