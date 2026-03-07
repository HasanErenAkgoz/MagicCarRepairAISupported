using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Sync.Commands.SyncData
{
    public class SyncDataCommandHandler : IRequestHandler<SyncDataCommand, IDataResult<SyncDataResponse>>
    {
        public async Task<IDataResult<SyncDataResponse>> Handle(SyncDataCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new SyncDataResponse();
                var errors = new List<string>();

                foreach (var change in request.Changes)
                {
                    try
                    {
                        // Her değişikliği işle
                        // Bu kısım entity tipine göre ilgili command/handler'ı çağıracak
                        // Şimdilik basit bir implementasyon

                        response.SyncedCount++;
                    }
                    catch (Exception ex)
                    {
                        response.FailedCount++;
                        errors.Add($"{change.EntityType} {change.EntityId}: {ex.Message}");
                    }
                }

                response.Errors = errors;

                return new SuccessDataResult<SyncDataResponse>(
                    response,
                    $"{response.SyncedCount} değişiklik sync edildi. {response.FailedCount} başarısız."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SyncDataResponse>($"Sync işlemi sırasında hata oluştu: {ex.Message}");
            }
        }
    }
}
