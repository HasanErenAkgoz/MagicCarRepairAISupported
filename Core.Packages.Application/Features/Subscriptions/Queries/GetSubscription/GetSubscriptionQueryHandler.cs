using MagicCarRepairAISupported.Application.Common.Services.Subscription;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Queries.GetSubscription
{
    public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, IDataResult<GetSubscriptionResponse>>
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<GetSubscriptionQueryHandler> _logger;

        public GetSubscriptionQueryHandler(
            ISubscriptionService subscriptionService,
            ILogger<GetSubscriptionQueryHandler> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        public async Task<IDataResult<GetSubscriptionResponse>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subscription = await _subscriptionService.GetCurrentSubscriptionAsync(request.ClientId);
                if (subscription == null)
                {
                    return new ErrorDataResult<GetSubscriptionResponse>("Abonelik bulunamadı.");
                }

                var features = new List<string>();
                if (!string.IsNullOrEmpty(subscription.Features))
                {
                    features = JsonSerializer.Deserialize<List<string>>(subscription.Features) ?? new List<string>();
                }

                var response = new GetSubscriptionResponse
                {
                    SubscriptionId = subscription.Id,
                    Plan = subscription.Plan.ToString(),
                    Status = subscription.Status.ToString(),
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate,
                    MonthlyPrice = subscription.MonthlyPrice,
                    YearlyPrice = subscription.YearlyPrice,
                    MaxWorkOrders = subscription.MaxWorkOrders,
                    MaxUsers = subscription.MaxUsers,
                    MaxAIAnalyses = subscription.MaxAIAnalyses,
                    Features = features,
                    AutoRenew = subscription.AutoRenew,
                    IsActive = subscription.IsActive(),
                    NextPaymentDate = subscription.NextPaymentDate
                };

                return new SuccessDataResult<GetSubscriptionResponse>(response, "Abonelik bilgileri başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription for client {ClientId}", request.ClientId);
                return new ErrorDataResult<GetSubscriptionResponse>("Abonelik bilgileri getirilirken bir hata oluştu.");
            }
        }
    }
}
