using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Subscription;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.CancelSubscription
{
    public class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand, IDataResult<CancelSubscriptionResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ITenantService _tenantService;
        private readonly ILogger<CancelSubscriptionCommandHandler> _logger;

        public CancelSubscriptionCommandHandler(
            ISubscriptionRepository subscriptionRepository,
            ITenantService tenantService,
            ILogger<CancelSubscriptionCommandHandler> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<IDataResult<CancelSubscriptionResponse>> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Mevcut aktif aboneliği getir
                var subscription = await _subscriptionRepository.GetActiveSubscriptionAsync(request.ClientId);
                if (subscription == null)
                {
                    return new ErrorDataResult<CancelSubscriptionResponse>("Aktif abonelik bulunamadı.");
                }

                // Aboneliği iptal et
                subscription.Status = SubscriptionStatus.Cancelled;
                subscription.CancelledDate = DateTime.UtcNow;
                subscription.CancellationReason = request.Reason ?? "Kullanıcı tarafından iptal edildi";
                subscription.AutoRenew = false;

                _subscriptionRepository.Update(subscription);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("Subscription cancelled for client {ClientId}, subscription {SubscriptionId}", 
                    request.ClientId, subscription.Id);

                return new SuccessDataResult<CancelSubscriptionResponse>(new CancelSubscriptionResponse
                {
                    SubscriptionId = subscription.Id,
                    CancelledDate = subscription.CancelledDate.Value,
                    Message = "Abonelik başarıyla iptal edildi. Mevcut abonelik süresi bitene kadar kullanmaya devam edebilirsiniz."
                }, "Abonelik başarıyla iptal edildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling subscription for client {ClientId}", request.ClientId);
                return new ErrorDataResult<CancelSubscriptionResponse>("Abonelik iptal edilirken bir hata oluştu.");
            }
        }
    }
}
