using MagicCarRepairAISupported.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            // Seed Clients
            modelBuilder.Entity<Domain.Entities.Client>()
                .HasData(ClientSeedData.GetClients());

            // Seed Error Messages
            modelBuilder.Entity<Domain.Entities.ErrorMessage>()
                .HasData(ErrorMessageSeedData.GetErrorMessages());

            // Seed Global Permissions (ClientId = 0)
            modelBuilder.Entity<Domain.Entities.Permission>()
                .HasData(RolePermissionSeedData.GetGlobalPermissions());

            // Seed Roles for each client
            modelBuilder.Entity<Domain.Entities.Role>()
                .HasData(RolePermissionSeedData.GetDemoClientRoles());
            
            modelBuilder.Entity<Domain.Entities.Role>()
                .HasData(RolePermissionSeedData.GetTestClientRoles());

            // Seed Role-Permission mappings for each client
            modelBuilder.Entity<Domain.Entities.RolePermission>()
                .HasData(RolePermissionSeedData.GetDemoClientRolePermissions());
            
            modelBuilder.Entity<Domain.Entities.RolePermission>()
                .HasData(RolePermissionSeedData.GetTestClientRolePermissions());

            // Insurance Companies (Global - ClientId = 0)
            modelBuilder.Entity<Domain.Entities.InsuranceCompany>()
                .HasData(InsuranceCompanySeedData.GetInsuranceCompanies());

            // Seed Employees
            modelBuilder.SeedEmployees();
        }
    }
}

