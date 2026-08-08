namespace MagicCarRepairAISupported.WebAPI.Authorization;

/// <summary>
/// Authorization policy names aligned with JWT UserType claim (1=SA, 2=Manager, 3=Employee, 4=Customer).
/// </summary>
public static class AuthPolicyNames
{
    /// <summary>SystemAdmin, Manager, or Employee.</summary>
    public const string ShopStaff = "ShopStaff";

    /// <summary>Customer (vehicle owner) only.</summary>
    public const string CustomerOnly = "CustomerOnly";

    /// <summary>Customer or SystemAdmin (support / impersonation flows).</summary>
    public const string CustomerOrSystemAdmin = "CustomerOrSystemAdmin";

    /// <summary>SystemAdmin only.</summary>
    public const string SystemAdminOnly = "SystemAdminOnly";
}
