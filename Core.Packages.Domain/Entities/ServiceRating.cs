using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Servis değerlendirme (Rating) entity'si
    /// </summary>
    public class ServiceRating : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş Emri ID
        /// </summary>
        public int WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Değerlendirilen Servis (Client) ID
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Genel Puan (1-5 yıldız)
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Hizmet Kalitesi (1-5)
        /// </summary>
        public int ServiceQuality { get; set; }

        /// <summary>
        /// Fiyat Uygunluğu (1-5)
        /// </summary>
        public int PriceValue { get; set; }

        /// <summary>
        /// Zamanında Teslim (1-5)
        /// </summary>
        public int OnTimeDelivery { get; set; }

        /// <summary>
        /// Personel Davranışı (1-5)
        /// </summary>
        public int StaffBehavior { get; set; }

        /// <summary>
        /// Yorum (Text)
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Fotoğraflar (JSON formatında - file paths)
        /// </summary>
        public string? Photos { get; set; }

        /// <summary>
        /// Durum (Onaylandı, Beklemede, Reddedildi)
        /// </summary>
        public RatingStatus Status { get; set; } = RatingStatus.Pending;

        /// <summary>
        /// Servis Yanıtı
        /// </summary>
        public string? ServiceReply { get; set; }

        /// <summary>
        /// Servis Yanıt Tarihi
        /// </summary>
        public DateTime? ServiceReplyDate { get; set; }

        /// <summary>
        /// Ortalama puan hesapla
        /// </summary>
        public decimal CalculateAverageRating()
        {
            return (ServiceQuality + PriceValue + OnTimeDelivery + StaffBehavior) / 4.0m;
        }
    }
}
