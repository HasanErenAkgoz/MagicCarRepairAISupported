using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Queries.GetSubscriptionPlans
{
    public class GetSubscriptionPlansQueryHandler : IRequestHandler<GetSubscriptionPlansQuery, IDataResult<List<SubscriptionPlanDto>>>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetSubscriptionPlansQueryHandler> _logger;

        public GetSubscriptionPlansQueryHandler(
            IConfiguration configuration,
            ILogger<GetSubscriptionPlansQueryHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IDataResult<List<SubscriptionPlanDto>>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var plans = new List<SubscriptionPlanDto>();

                // Free Plan
                plans.Add(new SubscriptionPlanDto
                {
                    Plan = SubscriptionPlan.Free.ToString(),
                    Name = "Ücretsiz",
                    Description = "Temel özellikler, 10 iş emri/ay, 1 kullanıcı",
                    MonthlyPrice = 0,
                    YearlyPrice = 0,
                    MaxWorkOrders = 10,
                    MaxUsers = 1,
                    MaxAIAnalyses = 0,
                    Features = new List<string>(),
                    IsPopular = false
                });

                // Basic Plan
                var basicConfig = _configuration.GetSection("Subscription:Plans:Basic");
                plans.Add(new SubscriptionPlanDto
                {
                    Plan = SubscriptionPlan.Basic.ToString(),
                    Name = "Temel",
                    Description = "50 iş emri/ay, 2 kullanıcı, temel raporlar",
                    MonthlyPrice = basicConfig.GetValue<decimal>("MonthlyPrice", 299),
                    YearlyPrice = basicConfig.GetValue<decimal?>("YearlyPrice", 2870),
                    MaxWorkOrders = basicConfig.GetValue<int>("MaxWorkOrders", 50),
                    MaxUsers = basicConfig.GetValue<int>("MaxUsers", 2),
                    MaxAIAnalyses = basicConfig.GetValue<int>("MaxAIAnalyses", 0),
                    Features = basicConfig.GetSection("Features").Get<List<string>>() ?? new List<string>(),
                    IsPopular = false
                });

                // Professional Plan
                var professionalConfig = _configuration.GetSection("Subscription:Plans:Professional");
                plans.Add(new SubscriptionPlanDto
                {
                    Plan = SubscriptionPlan.Professional.ToString(),
                    Name = "Profesyonel",
                    Description = "Sınırsız iş emri, 5 kullanıcı, AI analiz (10/ay), gelişmiş raporlar",
                    MonthlyPrice = professionalConfig.GetValue<decimal>("MonthlyPrice", 799),
                    YearlyPrice = professionalConfig.GetValue<decimal?>("YearlyPrice", 7670),
                    MaxWorkOrders = professionalConfig.GetValue<int>("MaxWorkOrders", -1),
                    MaxUsers = professionalConfig.GetValue<int>("MaxUsers", 5),
                    MaxAIAnalyses = professionalConfig.GetValue<int>("MaxAIAnalyses", 10),
                    Features = professionalConfig.GetSection("Features").Get<List<string>>() ?? new List<string> { "AdvancedReports", "AIAnalysis" },
                    IsPopular = true
                });

                // Enterprise Plan
                var enterpriseConfig = _configuration.GetSection("Subscription:Plans:Enterprise");
                plans.Add(new SubscriptionPlanDto
                {
                    Plan = SubscriptionPlan.Enterprise.ToString(),
                    Name = "Kurumsal",
                    Description = "Sınırsız her şey, sınırsız kullanıcı, özel destek, API erişimi",
                    MonthlyPrice = enterpriseConfig.GetValue<decimal>("MonthlyPrice", 1999),
                    YearlyPrice = enterpriseConfig.GetValue<decimal?>("YearlyPrice", 19190),
                    MaxWorkOrders = enterpriseConfig.GetValue<int>("MaxWorkOrders", -1),
                    MaxUsers = enterpriseConfig.GetValue<int>("MaxUsers", -1),
                    MaxAIAnalyses = enterpriseConfig.GetValue<int>("MaxAIAnalyses", -1),
                    Features = enterpriseConfig.GetSection("Features").Get<List<string>>() ?? new List<string> { "AdvancedReports", "APIAccess", "WhiteLabel", "PrioritySupport", "AIAnalysis" },
                    IsPopular = false
                });

                return new SuccessDataResult<List<SubscriptionPlanDto>>(plans, "Abonelik planları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription plans");
                return new ErrorDataResult<List<SubscriptionPlanDto>>("Abonelik planları getirilirken bir hata oluştu.");
            }
        }
    }
}
