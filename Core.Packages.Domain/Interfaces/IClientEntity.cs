namespace MagicCarRepairAISupported.Domain.Interfaces
{
    /// <summary>
    /// Interface for entities that support multi-tenancy
    /// </summary>
    public interface IClientEntity
    {
        int ClientId { get; set; }
    }
}

