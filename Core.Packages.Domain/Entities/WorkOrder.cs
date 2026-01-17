using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;
using System.Linq;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// İş emri entity'si
    /// </summary>
    public class WorkOrder : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş emri numarası (Otomatik oluşturulur: WO-YYYYMMDD-XXXX)
        /// </summary>
        public string WorkOrderNumber { get; set; }

        /// <summary>
        /// Araç ID
        /// </summary>
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Giriş Tarihi/Saati
        /// </summary>
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Tahmini Çıkış Tarihi
        /// </summary>
        public DateTime? EstimatedDeliveryDate { get; set; }

        /// <summary>
        /// Gerçek Çıkış Tarihi
        /// </summary>
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public new WorkOrderStatus Status { get; set; } = WorkOrderStatus.AppointmentScheduled;

        /// <summary>
        /// Öncelik
        /// </summary>
        public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Normal;

        /// <summary>
        /// Km Bilgisi (Araç giriş anındaki km)
        /// </summary>
        public long? Kilometers { get; set; }

        /// <summary>
        /// Yakıt Seviyesi (0-100)
        /// </summary>
        public int? FuelLevel { get; set; }

        /// <summary>
        /// Müşteri Şikayetleri (JSON formatında)
        /// </summary>
        public string? CustomerComplaints { get; set; }

        /// <summary>
        /// Özel İstekler (JSON formatında)
        /// </summary>
        public string? SpecialRequests { get; set; }

        /// <summary>
        /// Ara Toplam (Parça + İşçilik + Dış Hizmet)
        /// </summary>
        public decimal SubTotal { get; set; } = 0;

        /// <summary>
        /// İndirim Tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; } = 0;

        /// <summary>
        /// İndirim Yüzdesi
        /// </summary>
        public decimal DiscountPercentage { get; set; } = 0;

        /// <summary>
        /// KDV Tutarı
        /// </summary>
        public decimal TaxAmount { get; set; } = 0;

        /// <summary>
        /// Toplam Tutar
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// Ödenen Tutar
        /// </summary>
        public decimal PaidAmount { get; set; } = 0;

        /// <summary>
        /// Ödeme Durumu
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        /// <summary>
        /// Müşteri Onay Durumu
        /// </summary>
        public CustomerApprovalStatus? CustomerApprovalStatus { get; set; }

        /// <summary>
        /// Müşteri Onay Tarihi
        /// </summary>
        public DateTime? CustomerApprovalDate { get; set; }

        /// <summary>
        /// Müşteri Red Nedeni
        /// </summary>
        public string? CustomerRejectionReason { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Sorumlu Personel ID (İş emrini yöneten)
        /// </summary>
        public int? AssignedEmployeeId { get; set; }
        public virtual Employee? AssignedEmployee { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        // Navigation properties
        public virtual ICollection<WorkOrderItem> Items { get; set; } = new List<WorkOrderItem>();
        public virtual ICollection<WorkOrderLabor> Labors { get; set; } = new List<WorkOrderLabor>();
        public virtual ICollection<WorkOrderTimeline> Timeline { get; set; } = new List<WorkOrderTimeline>();
        public virtual ICollection<WorkOrderPhoto> Photos { get; set; } = new List<WorkOrderPhoto>();

        /// <summary>
        /// İş emri numarası oluşturur
        /// </summary>
        public static string GenerateWorkOrderNumber()
        {
            return $"WO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper()}";
        }

        /// <summary>
        /// Toplam tutarı hesaplar
        /// </summary>
        public void CalculateTotal()
        {
            // Ara toplam hesapla
            SubTotal = Items.Sum(i => i.TotalAmount) + Labors.Sum(l => l.TotalAmount);

            // İndirim uygula
            if (DiscountPercentage > 0)
            {
                DiscountAmount = SubTotal * (DiscountPercentage / 100);
            }

            // KDV hesapla (indirim sonrası)
            var amountAfterDiscount = SubTotal - DiscountAmount;
            TaxAmount = amountAfterDiscount * 0.20m; // %20 KDV (varsayılan)

            // Toplam tutar
            TotalAmount = amountAfterDiscount + TaxAmount;
        }

        /// <summary>
        /// Ödeme durumunu günceller
        /// </summary>
        public void UpdatePaymentStatus()
        {
            if (PaidAmount == 0)
            {
                PaymentStatus = PaymentStatus.Unpaid;
            }
            else if (PaidAmount >= TotalAmount)
            {
                PaymentStatus = PaymentStatus.Paid;
            }
            else
            {
                PaymentStatus = PaymentStatus.PartiallyPaid;
            }
        }

        /// <summary>
        /// Durum değişikliği yapar
        /// </summary>
        public void ChangeStatus(WorkOrderStatus newStatus, int? employeeId = null, string? note = null)
        {
            var oldStatus = Status;
            Status = newStatus;

            // Durum değişikliğine göre tarihleri güncelle
            if (newStatus == WorkOrderStatus.Delivered && ActualDeliveryDate == null)
            {
                ActualDeliveryDate = DateTime.UtcNow;
            }
        }
    }
}

