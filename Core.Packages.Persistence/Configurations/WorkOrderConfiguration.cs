using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.ToTable("WorkOrders");

            builder.HasKey(w => w.Id);

            builder.Property(w => w.WorkOrderNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(w => w.EntryDate)
                .IsRequired();

            builder.Property(w => w.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(WorkOrderStatus.AppointmentScheduled);

            builder.Property(w => w.Priority)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(WorkOrderPriority.Normal);

            builder.Property(w => w.FuelLevel)
                .HasMaxLength(3); // 0-100

            builder.Property(w => w.CustomerComplaints)
                .HasColumnType("nvarchar(max)"); // JSON

            builder.Property(w => w.SpecialRequests)
                .HasColumnType("nvarchar(max)"); // JSON

            builder.Property(w => w.SubTotal)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.DiscountAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.DiscountPercentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.TaxAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.PaidAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(w => w.PaymentStatus)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(PaymentStatus.Unpaid);

            builder.Property(w => w.Notes)
                .HasMaxLength(2000);

            // Indexes
            builder.HasIndex(w => w.WorkOrderNumber).IsUnique();
            builder.HasIndex(w => w.ClientId);
            builder.HasIndex(w => w.VehicleId);
            builder.HasIndex(w => w.CustomerId);
            builder.HasIndex(w => w.Status);
            builder.HasIndex(w => w.Priority);
            builder.HasIndex(w => w.EntryDate);
            builder.HasIndex(w => w.AssignedEmployeeId);
            builder.HasIndex(w => w.PaymentStatus);

            // Composite indexes for query optimization
            builder.HasIndex(w => new { w.ClientId, w.Status }); // Active workorders query
            builder.HasIndex(w => new { w.ClientId, w.EntryDate, w.Status }); // Date range queries
            builder.HasIndex(w => new { w.ClientId, w.AssignedEmployeeId, w.Status }); // Employee performance
            builder.HasIndex(w => new { w.ClientId, w.CustomerId, w.Status }); // Customer workorders
            builder.HasIndex(w => new { w.ClientId, w.Priority, w.Status }); // Priority filtering

            // Relationships
            builder.HasOne(w => w.Client)
                .WithMany()
                .HasForeignKey(w => w.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.Vehicle)
                .WithMany()
                .HasForeignKey(w => w.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.Customer)
                .WithMany()
                .HasForeignKey(w => w.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(w => w.AssignedEmployee)
                .WithMany()
                .HasForeignKey(w => w.AssignedEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Navigation properties
            builder.HasMany(w => w.Items)
                .WithOne(i => i.WorkOrder)
                .HasForeignKey(i => i.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Labors)
                .WithOne(l => l.WorkOrder)
                .HasForeignKey(l => l.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Timeline)
                .WithOne(t => t.WorkOrder)
                .HasForeignKey(t => t.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Photos)
                .WithOne(p => p.WorkOrder)
                .HasForeignKey(p => p.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

