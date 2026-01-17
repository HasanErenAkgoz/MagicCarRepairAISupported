namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// İş emri kalem tipleri
    /// </summary>
    public enum WorkOrderItemType
    {
        /// <summary>
        /// Parça
        /// </summary>
        Part = 1,

        /// <summary>
        /// İşçilik
        /// </summary>
        Labor = 2,

        /// <summary>
        /// Dış Hizmet (Subcontractor)
        /// </summary>
        ExternalService = 3,

        /// <summary>
        /// Diğer
        /// </summary>
        Other = 4
    }
}

