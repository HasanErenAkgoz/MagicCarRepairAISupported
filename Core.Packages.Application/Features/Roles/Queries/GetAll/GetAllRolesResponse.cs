namespace MagicCarRepairAISupported.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public int PermissionCount { get; set; }
        public List<RolePermissionItem> Permissions { get; set; } = new();
        public int UserCount { get; set; }
    }

    public class RolePermissionItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
