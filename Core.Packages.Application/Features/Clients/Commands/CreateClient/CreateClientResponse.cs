namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ContactEmail { get; set; }
        public bool IsActive { get; set; }
    }
}

