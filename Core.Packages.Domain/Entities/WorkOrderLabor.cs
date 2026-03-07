using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// İş emri işçilik entity'si
    /// </summary>
    public class WorkOrderLabor : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş emri ID
        /// </summary>
        public int WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Personel ID (İşi yapan)
        /// </summary>
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// İşlem Adı
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Başlangıç Saati
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Bitiş Saati
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Süre (Saat)
        /// </summary>
        public decimal? DurationHours { get; set; }

        /// <summary>
        /// Saat Ücreti
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Toplam Tutar (Süre * Saat Ücreti)
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Süreyi hesaplar (StartTime ve EndTime'dan)
        /// </summary>
        public void CalculateDuration()
        {
            if (StartTime.HasValue && EndTime.HasValue)
            {
                var duration = EndTime.Value - StartTime.Value;
                DurationHours = (decimal)duration.TotalHours;
            }
        }

        /// <summary>
        /// Toplam tutarı hesaplar
        /// </summary>
        public void CalculateTotal()
        {
            if (DurationHours.HasValue)
            {
                TotalAmount = DurationHours.Value * HourlyRate;
            }
            else if (StartTime.HasValue && EndTime.HasValue)
            {
                CalculateDuration();
                if (DurationHours.HasValue)
                {
                    TotalAmount = DurationHours.Value * HourlyRate;
                }
            }
        }
    }
}

