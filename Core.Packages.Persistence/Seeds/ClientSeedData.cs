using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Persistence.Seeds
{
    public static class ClientSeedData
    {
        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    Id = 1,
                    Name = "Demo Client",
                    Code = "DEMO001",
                    Description = "Demo tenant for testing",
                    ContactEmail = "demo@example.com",
                    ContactPhone = "+90 555 123 4567",
                    IsActive = true,
                    SubscriptionStartDate = DateTime.UtcNow,
                    SubscriptionEndDate = DateTime.UtcNow.AddYears(1),
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                },
                new Client
                {
                    Id = 2,
                    Name = "Test Client",
                    Code = "TEST001",
                    Description = "Test tenant for development",
                    ContactEmail = "test@example.com",
                    ContactPhone = "+90 555 987 6543",
                    IsActive = true,
                    SubscriptionStartDate = DateTime.UtcNow,
                    SubscriptionEndDate = DateTime.UtcNow.AddYears(1),
                    CreatedDate = DateTime.UtcNow,
                    Status = Status.Active
                }
            };
        }
    }
}

