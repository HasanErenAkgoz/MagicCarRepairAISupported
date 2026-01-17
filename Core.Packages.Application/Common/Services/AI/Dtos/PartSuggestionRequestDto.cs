namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Parça önerisi isteği DTO
    /// </summary>
    public class PartSuggestionRequestDto
    {
        /// <summary>
        /// İş Emri ID (null ise genel öneriler yapılır)
        /// </summary>
        public int? WorkOrderId { get; set; }

        /// <summary>
        /// Araç ID (iş emri yoksa)
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// Müşteri Şikayeti/Açıklama
        /// </summary>
        public string? CustomerComplaint { get; set; }

        /// <summary>
        /// Parça Kategori (opsiyonel - filtreleme için)
        /// </summary>
        public string? PartCategory { get; set; }

        /// <summary>
        /// Önerilecek parça sayısı (varsayılan: 10)
        /// </summary>
        public int NumberOfSuggestions { get; set; } = 10;

        /// <summary>
        /// Stokta olan parçaları tercih et
        /// </summary>
        public bool PreferInStock { get; set; } = true;

        /// <summary>
        /// Fiyat aralığı (min)
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Fiyat aralığı (max)
        /// </summary>
        public decimal? MaxPrice { get; set; }
    }
}
