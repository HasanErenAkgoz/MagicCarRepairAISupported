namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddPhoto
{
    public class AddWorkOrderPhotoResponse
    {
        public int PhotoId { get; set; }
        public int WorkOrderId { get; set; }
        public string FilePath { get; set; }
    }
}

