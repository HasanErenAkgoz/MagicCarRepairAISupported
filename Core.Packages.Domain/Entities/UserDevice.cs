using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Kullanıcı cihaz bilgilerini saklar (Remember Me özelliği için)
    /// </summary>
    public class UserDevice : BaseEntity<int>, IClientEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// Unique device identifier (Frontend'den gönderilecek)
        /// </summary>
        public string DeviceId { get; set; } = string.Empty;

        /// <summary>
        /// Cihaz adı (örn: "iPhone 13", "Samsung Galaxy S21")
        /// </summary>
        public string? DeviceName { get; set; }

        /// <summary>
        /// Remember Me ile işaretlenmiş mi?
        /// </summary>
        public bool IsTrusted { get; set; } = false;

        /// <summary>
        /// Son login tarihi
        /// </summary>
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
