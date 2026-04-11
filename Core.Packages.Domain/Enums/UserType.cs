using MagicCarRepairAISupported.Domain.Converters;
using System.Text.Json.Serialization;

namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Kullanıcı tipi
    /// </summary>
    [JsonConverter(typeof(UserTypeJsonConverter))]
    public enum UserType
    {
        /// <summary>
        /// Sistem Yöneticisi (Tüm sistemin admini)
        /// </summary>
        SystemAdmin = 1,

        /// <summary>
        /// İşletme Sahibi/Yönetici
        /// </summary>
        Manager = 2,

        /// <summary>
        /// Personel (Çalışan)
        /// </summary>
        Employee = 3,

        /// <summary>
        /// Müşteri
        /// </summary>
        Customer = 4
    }
}

