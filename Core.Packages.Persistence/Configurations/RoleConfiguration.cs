using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            
            // Multi-tenant: replace Identity's global NormalizedName unique index
            builder.HasIndex(x => x.NormalizedName)
                .HasDatabaseName("RoleNameIndex")
                .IsUnique(false);

            builder.HasIndex(x => new { x.NormalizedName, x.ClientId })
                .IsUnique()
                .HasDatabaseName("IX_Roles_NormalizedName_ClientId");

            builder.HasIndex(x => new { x.Name, x.ClientId }).IsUnique();
            builder.HasIndex(x => x.ClientId);
            
            // Relationship to Client
            builder.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
