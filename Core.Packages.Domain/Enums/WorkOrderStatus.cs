namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// İş emri durumları
    /// </summary>
    public enum WorkOrderStatus
    {
        /// <summary>
        /// Randevu Alındı
        /// </summary>
        AppointmentScheduled = 1,

        /// <summary>
        /// Araç Girişi Yapıldı
        /// </summary>
        VehicleEntered = 2,

        /// <summary>
        /// Arıza Tespit Edildi
        /// </summary>
        DiagnosisCompleted = 3,

        /// <summary>
        /// Parça Bekliyor
        /// </summary>
        WaitingForParts = 4,

        /// <summary>
        /// İşlem Başladı
        /// </summary>
        InProgress = 5,

        /// <summary>
        /// İşlem Devam Ediyor
        /// </summary>
        InRepair = 6,

        /// <summary>
        /// Kalite Kontrol
        /// </summary>
        QualityControl = 7,

        /// <summary>
        /// Yıkama
        /// </summary>
        Washing = 8,

        /// <summary>
        /// Teslime Hazır
        /// </summary>
        ReadyForDelivery = 9,

        /// <summary>
        /// Teslim Edildi
        /// </summary>
        Delivered = 10,

        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 11
    }
}

