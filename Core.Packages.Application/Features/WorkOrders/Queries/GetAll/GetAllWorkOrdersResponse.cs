using MagicCarRepairAISupported.Domain.Enums;
using System;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll
{
    public class GetAllWorkOrdersResponse
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public int VehicleId { get; set; }
        public VehicleDto Vehicle { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public WorkOrderStatus Status { get; set; }
        public string StatusName { get; set; }
        public WorkOrderPriority Priority { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? EstimatedCost { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusName { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? AssignedEmployeeName { get; set; }
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string? Avatar { get; set; }
    }

    public class VehicleDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
    }
}
