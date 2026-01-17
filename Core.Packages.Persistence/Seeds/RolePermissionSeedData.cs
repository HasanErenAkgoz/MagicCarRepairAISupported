using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class RolePermissionSeedData
    {
        /// <summary>
        /// Global Permissions - Available to all clients (ClientId = 0)
        /// </summary>
        public static List<Permission> GetGlobalPermissions()
        {
            return new List<Permission>
            {
                new Permission { Id = 1, Name = "Customers.View", Description = "View customers", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 2, Name = "Customers.Create", Description = "Create customers", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 3, Name = "Customers.Update", Description = "Update customers", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 4, Name = "Customers.Delete", Description = "Delete customers", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                
                new Permission { Id = 5, Name = "Vehicles.View", Description = "View vehicles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 6, Name = "Vehicles.Create", Description = "Create vehicles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 7, Name = "Vehicles.Update", Description = "Update vehicles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 8, Name = "Vehicles.Delete", Description = "Delete vehicles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                
                new Permission { Id = 9, Name = "Users.View", Description = "View users", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 10, Name = "Users.Create", Description = "Create users", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 11, Name = "Users.Update", Description = "Update users", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 12, Name = "Users.Delete", Description = "Delete users", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                
                new Permission { Id = 13, Name = "Roles.View", Description = "View roles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 14, Name = "Roles.Create", Description = "Create roles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 15, Name = "Roles.Update", Description = "Update roles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
                new Permission { Id = 16, Name = "Roles.Delete", Description = "Delete roles", CreatedDate = DateTime.UtcNow, Status = Status.Active },
            };
        }

        /// <summary>
        /// Demo Client (ClientId = 1) - Roles
        /// </summary>
        public static List<Role> GetDemoClientRoles()
        {
            return new List<Role>
            {
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ClientId = 1, ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Id = 2, Name = "Manager", NormalizedName = "MANAGER", ClientId = 1, ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Id = 3, Name = "Technician", NormalizedName = "TECHNICIAN", ClientId = 1, ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Id = 4, Name = "Receptionist", NormalizedName = "RECEPTIONIST", ClientId = 1, ConcurrencyStamp = Guid.NewGuid().ToString() },
            };
        }

        /// <summary>
        /// Test Client (ClientId = 2) - Different Roles
        /// </summary>
        public static List<Role> GetTestClientRoles()
        {
            return new List<Role>
            {
                new Role { Id = 5, Name = "Owner", NormalizedName = "OWNER", ClientId = 2, ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Id = 6, Name = "ServiceAdvisor", NormalizedName = "SERVICEADVISOR", ClientId = 2, ConcurrencyStamp = Guid.NewGuid().ToString() },
                new Role { Id = 7, Name = "Mechanic", NormalizedName = "MECHANIC", ClientId = 2, ConcurrencyStamp = Guid.NewGuid().ToString() },
            };
        }

        /// <summary>
        /// Demo Client (ClientId = 1) - Role Permissions
        /// </summary>
        public static List<RolePermission> GetDemoClientRolePermissions()
        {
            var rolePermissions = new List<RolePermission>();
            int id = 1;

            // Admin role (ID: 1) - Full access
            for (int permissionId = 1; permissionId <= 16; permissionId++)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 1,
                    PermissionId = permissionId,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            // Manager role (ID: 2) - Most permissions except delete users/roles
            var managerPermissions = new[] { 1, 2, 3, 5, 6, 7, 9, 10, 11, 13, 14, 15 };
            foreach (var permId in managerPermissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 2,
                    PermissionId = permId,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            // Technician role (ID: 3) - View all, manage vehicles
            var technicianPermissions = new[] { 1, 5, 6, 7, 8, 9 };
            foreach (var permId in technicianPermissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 3,
                    PermissionId = permId,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            // Receptionist role (ID: 4) - View and create customers/vehicles
            var receptionistPermissions = new[] { 1, 2, 3, 5, 6, 7 };
            foreach (var permId in receptionistPermissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 4,
                    PermissionId = permId,
                    ClientId = 1,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            return rolePermissions;
        }

        /// <summary>
        /// Test Client (ClientId = 2) - Role Permissions (Different structure)
        /// </summary>
        public static List<RolePermission> GetTestClientRolePermissions()
        {
            var rolePermissions = new List<RolePermission>();
            int id = 100; // Start from 100 to avoid conflicts

            // Owner role (ID: 5) - Full access
            for (int permissionId = 1; permissionId <= 16; permissionId++)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 5,
                    PermissionId = permissionId,
                    ClientId = 2,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            // ServiceAdvisor role (ID: 6) - Customer and vehicle management
            var advisorPermissions = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var permId in advisorPermissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 6,
                    PermissionId = permId,
                    ClientId = 2,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            // Mechanic role (ID: 7) - View customers, manage vehicles
            var mechanicPermissions = new[] { 1, 5, 6, 7 };
            foreach (var permId in mechanicPermissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = id++,
                    RoleId = 7,
                    PermissionId = permId,
                    ClientId = 2,
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                });
            }

            return rolePermissions;
        }
    }
}

