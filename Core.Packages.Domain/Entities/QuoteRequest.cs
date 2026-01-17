using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Teklif talebi entity'si - Müşteriler servislerden teklif isteyebilir
    /// </summary>
    public class QuoteRequest : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Talep numarası (Otomatik oluşturulur: QR-YYYYMMDD-XXXX)
        /// </summary>
        public string RequestNumber { get; set; }

        /// <summary>
        /// Müşteri ID (Opsiyonel - misafir kullanıcılar için)
        /// </summary>
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Araç ID (Opsiyonel - yeni araç için)
        /// </summary>
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        /// <summary>
        /// Araç bilgileri (VehicleId yoksa kullanılır)
        /// </summary>
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleLicensePlate { get; set; }

        /// <summary>
        /// Sorun açıklaması
        /// </summary>
        public string ProblemDescription { get; set; }

        /// <summary>
        /// Talep tipi (Kaza, Arıza, Bakım, vb.)
        /// </summary>
        public QuoteRequestType RequestType { get; set; }

        /// <summary>
        /// Aciliyet seviyesi
        /// </summary>
        public UrgencyLevel UrgencyLevel { get; set; } = UrgencyLevel.Normal;

        /// <summary>
        /// İstenen başlangıç tarihi
        /// </summary>
        public DateTime? DesiredStartDate { get; set; }

        /// <summary>
        /// İstenen bitiş tarihi
        /// </summary>
        public DateTime? DesiredEndDate { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public QuoteStatus Status { get; set; } = QuoteStatus.Open;

        /// <summary>
        /// Seçilen teklif ID
        /// </summary>
        public int? SelectedQuoteResponseId { get; set; }
        public virtual QuoteResponse? SelectedQuoteResponse { get; set; }

        /// <summary>
        /// Son teklif tarihi (Bu tarihten sonra teklif alınamaz)
        /// </summary>
        public DateTime QuoteDeadline { get; set; }

        /// <summary>
        /// Müşteri iletişim bilgileri (Misafir kullanıcılar için)
        /// </summary>
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// İlişkili teklifler
        /// </summary>
        public virtual ICollection<QuoteResponse> QuoteResponses { get; set; } = new List<QuoteResponse>();

        /// <summary>
        /// Fotoğraflar
        /// </summary>
        public virtual ICollection<QuoteRequestPhoto> Photos { get; set; } = new List<QuoteRequestPhoto>();

        /// <summary>
        /// Talep numarası oluştur
        /// </summary>
        public void GenerateRequestNumber()
        {
            if (string.IsNullOrEmpty(RequestNumber))
            {
                var date = DateTime.UtcNow;
                RequestNumber = $"QR-{date:yyyyMMdd}-{Id:D4}";
            }
        }

        /// <summary>
        /// Durum güncelle
        /// </summary>
        public void UpdateStatus(QuoteStatus newStatus)
        {
            Status = newStatus;
        }

        /// <summary>
        /// Teklif seçildi
        /// </summary>
        public void SelectQuote(int quoteResponseId)
        {
            SelectedQuoteResponseId = quoteResponseId;
            Status = QuoteStatus.QuoteSelected;
        }

        /// <summary>
        /// Süresi doldu mu kontrol et
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.UtcNow > QuoteDeadline && Status == QuoteStatus.Open;
        }

        /// <summary>
        /// Teklif alınabilir mi kontrol et
        /// </summary>
        public bool CanAcceptQuotes()
        {
            return Status == QuoteStatus.Open && !IsExpired();
        }
    }
}

