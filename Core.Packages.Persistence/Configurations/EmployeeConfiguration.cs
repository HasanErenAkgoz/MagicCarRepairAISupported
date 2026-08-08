using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeNo)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.NationalId)
                .HasMaxLength(11);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Position)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.HireDate)
                .IsRequired();

            builder.Property(e => e.EmploymentStatus)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(EmploymentStatus.Active);

            builder.Property(e => e.Specializations)
                .HasColumnType("text"); // JSON

            builder.Property(e => e.Address)
                .HasMaxLength(500);

            builder.Property(e => e.BloodType)
                .HasMaxLength(5);

            builder.Property(e => e.EmergencyContact)
                .HasMaxLength(100);

            builder.Property(e => e.EmergencyPhone)
                .HasMaxLength(20);

            builder.Property(e => e.Notes)
                .HasMaxLength(1000);

            // Public profile fields
            builder.Property(e => e.Biography)
                .HasMaxLength(2000);

            builder.Property(e => e.ProfilePhotoUrl)
                .HasMaxLength(500);

            builder.Property(e => e.SpecializationsJson)
                .HasMaxLength(500);

            // User relationship (nullable)
            builder.HasIndex(e => e.UserId);

            // Multi-tenant
            builder.HasIndex(e => new { e.EmployeeNo, e.ClientId }).IsUnique();
            builder.HasIndex(e => e.ClientId);
            builder.HasIndex(e => e.Position);
            builder.HasIndex(e => e.EmploymentStatus);

            builder.HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Computed column
            builder.Ignore(e => e.FullName);
        }
    }
}

