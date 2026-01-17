using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.AppointmentNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.Property(a => a.StartTime)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(1000);

            builder.Property(a => a.CancellationReason)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(a => a.AppointmentNumber);
            builder.HasIndex(a => a.ClientId);
            builder.HasIndex(a => a.CustomerId);
            builder.HasIndex(a => new { a.AppointmentDate, a.StartTime });
            builder.HasIndex(a => a.AssignedEmployeeId);

            // Relationships
            builder.HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Vehicle)
                .WithMany()
                .HasForeignKey(a => a.VehicleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.AssignedEmployee)
                .WithMany()
                .HasForeignKey(a => a.AssignedEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}






