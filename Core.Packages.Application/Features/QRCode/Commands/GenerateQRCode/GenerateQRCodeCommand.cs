using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QRCode.Commands.GenerateQRCode
{
    public class GenerateQRCodeCommand : IRequest<IDataResult<GenerateQRCodeResponse>>
    {
        public string Data { get; set; } = string.Empty;
        public QRCodeType Type { get; set; }
        public int? RelatedEntityId { get; set; }
        public int? Size { get; set; } = 300;
    }

    public enum QRCodeType
    {
        WorkOrder = 1,
        Invoice = 2,
        Vehicle = 3,
        Customer = 4,
        QuickService = 5
    }

    public class GenerateQRCodeResponse
    {
        public string QRCodeUrl { get; set; } = string.Empty;
        public string QRCodeData { get; set; } = string.Empty;
    }
}
