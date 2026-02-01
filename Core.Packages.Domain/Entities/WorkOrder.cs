using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public class WorkOrder : BaseEntity<int>, IClientEntity
    {
        public string WorkOrderNumber { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public new WorkOrderStatus Status { get; set; } = WorkOrderStatus.AppointmentScheduled;
        public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Normal;
        public long? Kilometers { get; set; }
        public int? FuelLevel { get; set; }
        public string? CustomerComplaints { get; set; }
        public string? SpecialRequests { get; set; }
        public decimal SubTotal { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal DiscountPercentage { get; set; } = 0;
        public decimal TaxAmount { get; set; } = 0;
        public decimal TotalAmount { get; set; } = 0;
        public decimal? EstimatedCost { get; set; }
        public decimal PaidAmount { get; set; } = 0;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public CustomerApprovalStatus? CustomerApprovalStatus { get; set; }
        public DateTime? CustomerApprovalDate { get; set; }
        public string? CustomerRejectionReason { get; set; }
        public string? Notes { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public virtual Employee? AssignedEmployee { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual ICollection<WorkOrderItem> Items { get; set; } = new List<WorkOrderItem>();
        public virtual ICollection<WorkOrderLabor> Labors { get; set; } = new List<WorkOrderLabor>();
        public virtual ICollection<WorkOrderTimeline> Timeline { get; set; } = new List<WorkOrderTimeline>();
        public virtual ICollection<WorkOrderPhoto> Photos { get; set; } = new List<WorkOrderPhoto>();
        public static string GenerateWorkOrderNumber() { return "WO-" + DateTime.UtcNow.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper(); }
        public void CalculateTotal() { SubTotal = Items.Sum(i => i.TotalAmount) + Labors.Sum(l => l.TotalAmount); if (DiscountPercentage > 0) { DiscountAmount = SubTotal * (DiscountPercentage / 100); } var amountAfterDiscount = SubTotal - DiscountAmount; TaxAmount = amountAfterDiscount * 0.20m; TotalAmount = amountAfterDiscount + TaxAmount; }
        public void UpdatePaymentStatus() { if (PaidAmount == 0) { PaymentStatus = PaymentStatus.Unpaid; } else if (PaidAmount >= TotalAmount) { PaymentStatus = PaymentStatus.Paid; } else { PaymentStatus = PaymentStatus.PartiallyPaid; } }
        public void ChangeStatus(WorkOrderStatus newStatus, int? employeeId = null, string? note = null) { var oldStatus = Status; Status = newStatus; if (newStatus == WorkOrderStatus.Delivered && ActualDeliveryDate == null) { ActualDeliveryDate = DateTime.UtcNow; } }
    }
}
