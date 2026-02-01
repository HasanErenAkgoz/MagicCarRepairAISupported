namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus
{
    public class GetFleetStatusResponse
    {
        public int Repairing { get; set; }
        public int Completed { get; set; }
        public int Waiting { get; set; }
        public decimal Efficiency { get; set; }
    }
}
