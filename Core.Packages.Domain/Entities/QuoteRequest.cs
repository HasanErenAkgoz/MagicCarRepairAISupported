using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Fiyat teklifi isteği entity'si
    /// </summary>
    public class QuoteRequest : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Talep numarası (benzersiz)
        /// </summary>
        public string RequestNumber { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Müşteri adı (navigation olmadan erişim için)
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Araç ID (opsiyonel - müşteri henüz araç kaydetmemiş olabilir)
        /// </summary>
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        /// <summary>
        /// Araç bilgileri (navigation olmadan erişim için)
        /// </summary>
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehicleLicensePlate { get; set; }

        /// <summary>
        /// Fotoğraf yolları (JSON array formatında)
        /// </summary>
        public string PhotoPaths { get; set; } = "[]";

        /// <summary>
        /// Sorun açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Sorun açıklaması (alternatif property)
        /// </summary>
        public string? ProblemDescription
        {
            get => Description;
            set => Description = value;
        }

        /// <summary>
        /// Talep tipi
        /// </summary>
        public QuoteRequestType RequestType { get; set; } = QuoteRequestType.Other;

        /// <summary>
        /// Aciliyet seviyesi
        /// </summary>
        public UrgencyLevel UrgencyLevel { get; set; } = UrgencyLevel.Normal;

        /// <summary>
        /// AI teşhis akışından oluşturuldu mu?
        /// true ise QuoteDeadline ve QuoteResponse.ValidUntilDate 1 gündür.
        /// </summary>
        public bool IsAiGenerated { get; set; } = false;

        /// <summary>
        /// Teklif son tarihi (AI-generated: +1 gün, normal: +7 gün)
        /// </summary>
        public DateTime QuoteDeadline { get; set; } = DateTime.UtcNow.AddDays(7);

        /// <summary>
        /// Durum
        /// </summary>
        public new QuoteStatus Status { get; set; } = QuoteStatus.Open;

        /// <summary>
        /// AI tahmini maliyet (opsiyonel)
        /// </summary>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// AI tahmini minimum maliyet (aralık alt ucu)
        /// </summary>
        public decimal? EstimatedCostMin { get; set; }

        /// <summary>
        /// AI tahmini maksimum maliyet (aralık üst ucu)
        /// </summary>
        public decimal? EstimatedCostMax { get; set; }

        /// <summary>
        /// AI tahmin açıklaması
        /// </summary>
        public string? EstimatedDescription { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Teklifler (tamirhanelerin verdiği teklifler)
        /// </summary>
        public virtual ICollection<QuoteResponse> QuoteResponses { get; set; } = new List<QuoteResponse>();

        /// <summary>
        /// Müşteri email (misafir kullanıcı için)
        /// </summary>
        public string? CustomerEmail { get; set; }

        /// <summary>
        /// Müşteri telefon (misafir kullanıcı için)
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// Teklif alabilir durumda mı?
        /// </summary>
        public bool CanAcceptQuotes()
        {
            return Status == QuoteStatus.Open && QuoteDeadline > DateTime.UtcNow;
        }

        /// <summary>
        /// Durumu günceller
        /// </summary>
        public void UpdateStatus(QuoteStatus newStatus)
        {
            Status = newStatus;
        }

        /// <summary>
        /// Teklif seçer ve durumu günceller
        /// </summary>
        public void SelectQuote(int quoteResponseId)
        {
            Status = QuoteStatus.QuoteSelected;
        }
    }
}
