using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetByEmployee
{
    /// <summary>
    /// Personel bazlı iş emirleri response'u
    /// </summary>
    public class GetWorkOrdersByEmployeeResponse
    {
        /// <summary>
        /// Personel bilgileri
        /// </summary>
        public EmployeeInfo Employee { get; set; }

        /// <summary>
        /// İş emirleri listesi
        /// </summary>
        public List<WorkOrderInfo> WorkOrders { get; set; } = new();

        /// <summary>
        /// Toplam iş emri sayısı
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Aktif iş emri sayısı
        /// </summary>
        public int ActiveCount { get; set; }

        /// <summary>
        /// Tamamlanan iş emri sayısı
        /// </summary>
        public int CompletedCount { get; set; }

        /// <summary>
        /// Toplam gelir (tamamlanan iş emirlerinden)
        /// </summary>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Personel bilgileri
        /// </summary>
        public class EmployeeInfo
        {
            public int Id { get; set; }
            public string EmployeeNo { get; set; }
            public string FullName { get; set; }
            public string Position { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
        }

        /// <summary>
        /// İş emri bilgileri
        /// </summary>
        public class WorkOrderInfo
        {
            public int Id { get; set; }
            public string WorkOrderNumber { get; set; }
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public int VehicleId { get; set; }
            public string VehicleLicensePlate { get; set; }
            public string VehicleBrand { get; set; }
            public string VehicleModel { get; set; }
            public DateTime EntryDate { get; set; }
            public DateTime? EstimatedDeliveryDate { get; set; }
            public DateTime? ActualDeliveryDate { get; set; }
            public WorkOrderStatus Status { get; set; }
            public string StatusName { get; set; }
            public WorkOrderPriority Priority { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal PaidAmount { get; set; }
            public PaymentStatus PaymentStatus { get; set; }
            public string PaymentStatusName { get; set; }
            public string? Notes { get; set; }
        }
    }
}
