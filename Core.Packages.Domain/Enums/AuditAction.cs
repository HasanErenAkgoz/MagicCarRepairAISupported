namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Audit log işlem tipleri
    /// </summary>
    public enum AuditAction
    {
        Create = 1,
        Update = 2,
        Delete = 3,
        View = 4,
        Export = 5,
        Import = 6,
        Login = 7,
        Logout = 8,
        Approve = 9,
        Reject = 10,
        Cancel = 11,
        Complete = 12,
        Other = 99
    }
}
