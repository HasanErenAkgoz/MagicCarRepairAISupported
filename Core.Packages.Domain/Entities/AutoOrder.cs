using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Otomatik sipariş entity'si
    /// </summary>
    public class AutoOrder : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Sipariş numarası
        /// </summary>
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// Part ID
        /// </summary>
        public int PartId { get; set; }
        public virtual Part Part { get; set; }

        /// <summary>
        /// PartSupplier ID
        /// </summary>
        public int? PartSupplierId { get; set; }
        public virtual PartSupplier? PartSupplier { get; set; }

        /// <summary>
        /// StockAlert ID (hangi alarmdan oluşturuldu)
        /// </summary>
        public int? StockAlertId { get; set; }
        public virtual StockAlert? StockAlert { get; set; }

        /// <summary>
        /// Sipariş miktarı
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Toplam tutar
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Durum
        /// </summary>
        public AutoOrderStatus Status { get; set; } = AutoOrderStatus.Pending;

        /// <summary>
        /// Önerilen teslimat tarihi
        /// </summary>
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Gerçek teslimat tarihi
        /// </summary>
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
        /// Onaylayan kullanıcı ID
        /// </summary>
        public int? ApprovedByUserId { get; set; }
        public virtual User? ApprovedByUser { get; set; }

        /// <summary>
        /// Onay tarihi
        /// </summary>
        public DateTime? ApprovedDate { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Toplam tutarı hesapla
        /// </summary>
        public void CalculateTotal()
        {
            TotalAmount = Quantity * UnitPrice;
        }

        /// <summary>
        /// Siparişi onayla
        /// </summary>
        public void Approve(int userId)
        {
            Status = AutoOrderStatus.Approved;
            ApprovedByUserId = userId;
            ApprovedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Siparişi iptal et
        /// </summary>
        public void Cancel()
        {
            Status = AutoOrderStatus.Cancelled;
        }

        /// <summary>
        /// Sipariş numarası oluştur
        /// </summary>
        public static string GenerateOrderNumber()
        {
            return $"AO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
        }
    }
}

