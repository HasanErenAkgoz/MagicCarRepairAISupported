using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Stok alarm entity'si
    /// </summary>
    public class StockAlert : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Part ID
        /// </summary>
        public int PartId { get; set; }
        public virtual Part Part { get; set; }

        /// <summary>
        /// PartStock ID
        /// </summary>
        public int PartStockId { get; set; }
        public virtual PartStock PartStock { get; set; }

        /// <summary>
        /// Alarm tipi
        /// </summary>
        public StockAlertType AlertType { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public StockAlertStatus Status { get; set; } = StockAlertStatus.Active;

        /// <summary>
        /// Mevcut stok miktarı
        /// </summary>
        public decimal CurrentStock { get; set; }

        /// <summary>
        /// Minimum stok seviyesi
        /// </summary>
        public decimal MinimumStock { get; set; }

        /// <summary>
        /// Maksimum stok seviyesi
        /// </summary>
        public decimal? MaximumStock { get; set; }

        /// <summary>
        /// Önerilen sipariş miktarı
        /// </summary>
        public decimal? RecommendedOrderQuantity { get; set; }

        /// <summary>
        /// Alarm mesajı
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// İlk oluşturulma tarihi
        /// </summary>
        public DateTime FirstAlertDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Son güncelleme tarihi
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Çözülme tarihi
        /// </summary>
        public DateTime? ResolvedDate { get; set; }

        /// <summary>
        /// Otomatik sipariş oluşturuldu mu?
        /// </summary>
        public bool AutoOrderCreated { get; set; } = false;

        /// <summary>
        /// İlgili AutoOrder ID
        /// </summary>
        public int? AutoOrderId { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Alarmı çözüldü olarak işaretle
        /// </summary>
        public void Resolve()
        {
            Status = StockAlertStatus.Resolved;
            ResolvedDate = DateTime.UtcNow;
            LastUpdateDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Otomatik sipariş oluşturulduğunu işaretle
        /// </summary>
        public void MarkAutoOrderCreated(int autoOrderId)
        {
            AutoOrderCreated = true;
            AutoOrderId = autoOrderId;
            Status = StockAlertStatus.OrderCreated;
            LastUpdateDate = DateTime.UtcNow;
        }
    }
}

