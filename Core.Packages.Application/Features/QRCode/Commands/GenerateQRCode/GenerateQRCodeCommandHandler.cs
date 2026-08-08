using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.QRCode.Commands.GenerateQRCode
{
    public class GenerateQRCodeCommandHandler : IRequestHandler<GenerateQRCodeCommand, IDataResult<GenerateQRCodeResponse>>
    {
        private readonly IQrCodeService _qrCodeService;
        private readonly ITenantService _tenantService;

        public GenerateQRCodeCommandHandler(
            IQrCodeService qrCodeService,
            ITenantService tenantService)
        {
            _qrCodeService = qrCodeService;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<GenerateQRCodeResponse>> Handle(GenerateQRCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                // QR kod verisini oluştur
                var qrData = new
                {
                    type = request.Type.ToString(),
                    data = request.Data,
                    entityId = request.RelatedEntityId,
                    clientId = clientId,
                    timestamp = DateTime.UtcNow
                };

                var qrDataJson = JsonSerializer.Serialize(qrData);

                // QR kod oluştur (byte array olarak)
                var qrCodeBytes = _qrCodeService.GenerateQrCode(qrDataJson, request.Size ?? 300);
                
                // Base64 string'e çevir (frontend'de gösterim için)
                var qrCodeBase64 = Convert.ToBase64String(qrCodeBytes);
                var qrCodeUrl = $"data:image/png;base64,{qrCodeBase64}";

                return new SuccessDataResult<GenerateQRCodeResponse>(
                    new GenerateQRCodeResponse
                    {
                        QRCodeUrl = qrCodeUrl,
                        QRCodeData = qrDataJson
                    },
                    "QR kod başarıyla oluşturuldu."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GenerateQRCodeResponse>($"QR kod oluşturulurken hata oluştu: {ex.Message}");
            }
        }
    }
}
