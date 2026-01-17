namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Sigorta/Kasko türleri
    /// </summary>
    public enum InsuranceType
    {
        /// <summary>
        /// Kasko
        /// </summary>
        Comprehensive = 1,

        /// <summary>
        /// Trafik Sigortası (Zorunlu)
        /// </summary>
        TrafficInsurance = 2,

        /// <summary>
        /// Muafiyet
        /// </summary>
        Deductible = 3,

        /// <summary>
        /// Üçüncü Şahıs
        /// </summary>
        ThirdParty = 4,

        /// <summary>
        /// Diğer
        /// </summary>
        Other = 99
    }
}

