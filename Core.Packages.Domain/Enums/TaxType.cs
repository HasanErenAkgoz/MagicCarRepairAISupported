namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Vergi türleri
    /// </summary>
    public enum TaxType
    {
        /// <summary>
        /// KDV (Katma Değer Vergisi)
        /// </summary>
        VAT = 1,

        /// <summary>
        /// Stopaj
        /// </summary>
        WithholdingTax = 2,

        /// <summary>
        /// Kurumlar Vergisi
        /// </summary>
        CorporateTax = 3,

        /// <summary>
        /// Gelir Vergisi
        /// </summary>
        IncomeTax = 4,

        /// <summary>
        /// ÖTV (Özel Tüketim Vergisi)
        /// </summary>
        SpecialConsumptionTax = 5,

        /// <summary>
        /// Motorlu Taşıtlar Vergisi
        /// </summary>
        MotorVehicleTax = 6,

        /// <summary>
        /// Diğer
        /// </summary>
        Other = 99
    }
}
