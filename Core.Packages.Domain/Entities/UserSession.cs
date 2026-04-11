using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Kullanıcı oturum bilgilerini saklar (Session Management için)
    /// </summary>
    public class UserSession : BaseEntity<int>, IClientEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// JWT Token ID (JTI - JWT ID claim)
        /// </summary>
        public string TokenId { get; set; } = string.Empty;

        /// <summary>
        /// Device ID (UserDevice ile ilişkili)
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// Cihaz adı
        /// </summary>
        public string? DeviceName { get; set; }

        /// <summary>
        /// IP Adresi
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// User Agent (Browser/App bilgisi)
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// Remember Me ile mi oluşturuldu?
        /// </summary>
        public bool IsRemembered { get; set; } = false;

        /// <summary>
        /// Token expiration tarihi
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Son aktivite tarihi
        /// </summary>
        public DateTime LastActivityAt { get; set; } = DateTime.UtcNow;

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
