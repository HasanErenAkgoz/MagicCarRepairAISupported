using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Export;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Common.Services.Import;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Common.Services.WhatsApp;
using MagicCarRepairAISupported.Application.Common.Services.Subscription;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using MagicCarRepairAISupported.Infrastructure.Middlewares;
using MagicCarRepairAISupported.Infrastructure.Services.AI;
using MagicCarRepairAISupported.Infrastructure.Services.Auth;
using MagicCarRepairAISupported.Infrastructure.Services.Cache;
using MagicCarRepairAISupported.Infrastructure.Services.Email;
using MagicCarRepairAISupported.Infrastructure.Services.ErrorMessage;
using MagicCarRepairAISupported.Infrastructure.Services.Export;
using MagicCarRepairAISupported.Infrastructure.Services.FileUpload;
using MagicCarRepairAISupported.Infrastructure.Services.Import;
using MagicCarRepairAISupported.Infrastructure.Services.Invoice;
using MagicCarRepairAISupported.Infrastructure.Services.JWT;
using MagicCarRepairAISupported.Infrastructure.Services.Notification;
using MagicCarRepairAISupported.Infrastructure.Services.Payment;
using MagicCarRepairAISupported.Infrastructure.Services.SMS;
using MagicCarRepairAISupported.Infrastructure.Services.Subscription;
using MagicCarRepairAISupported.Infrastructure.Services.Stock;
using MagicCarRepairAISupported.Infrastructure.Services.Tenant;
using MagicCarRepairAISupported.Infrastructure.Services.WhatsApp;
using MagicCarRepairAISupported.Infrastructure.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace MagicCarRepairAISupported.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCoreInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configuration Options
            services.Configure<AIOptions>(configuration.GetSection("AIOptions"));

            // Memory Cache (for ErrorMessageService, TranslationService)
            services.AddMemoryCache();

            // Distributed Cache (for RedisService)
            services.AddDistributedMemoryCache(); // Fallback - Can be replaced with AddStackExchangeRedisCache in production

            // HttpClient Factory (for HTTP services)
            services.AddHttpClient(); // This registers IHttpClientFactory

            // Register HttpClient directly for services that inject it
            // Note: Using transient to ensure each service gets a new instance when needed
            // Better practice would be to refactor services to use IHttpClientFactory
            services.AddTransient<HttpClient>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                return httpClientFactory.CreateClient();
            });

            // AI Services - Config'e göre real veya mock
            var aiProvider = configuration["AIOptions:Provider"] ?? "Mock";
            
            if (aiProvider == "OpenAI" || aiProvider == "AzureOpenAI")
            {
                // Real AI implementations
                services.AddScoped<IAIDiagnosisService, OpenAIDiagnosisService>();
                services.AddScoped<IAIPhotoAnalysisService, OpenAIPhotoAnalysisService>();
                services.AddScoped<IAIPriceEstimationService, OpenAIPriceEstimationService>();
                services.AddScoped<IAIMaintenanceService, OpenAIMaintenanceService>();
                services.AddScoped<IAIChatService, OpenAIChatService>();
                services.AddScoped<IAppointmentOptimizationService, OpenAIAppointmentOptimizationService>();
                services.AddScoped<IStockForecastService, OpenAIStockForecastService>();
                services.AddScoped<ICustomerAnalysisService, OpenAICustomerAnalysisService>();
                services.AddScoped<IEmployeePerformanceAnalysisService, OpenAIEmployeePerformanceAnalysisService>();
                services.AddScoped<IPartSuggestionService, OpenAIPartSuggestionService>();
            }
            else
            {
                // Mock implementations for testing/development
                services.AddScoped<IAIDiagnosisService, MockAIDiagnosisService>();
                services.AddScoped<IAIPhotoAnalysisService, MockAIPhotoAnalysisService>();
                services.AddScoped<IAIPriceEstimationService, MockAIPriceEstimationService>();
                services.AddScoped<IAIMaintenanceService, MockAIMaintenanceService>();
                services.AddScoped<IAIChatService, MockAIChatService>();
                services.AddScoped<IAppointmentOptimizationService, MockAppointmentOptimizationService>();
                services.AddScoped<IStockForecastService, MockStockForecastService>();
                services.AddScoped<ICustomerAnalysisService, MockCustomerAnalysisService>();
                services.AddScoped<IEmployeePerformanceAnalysisService, MockEmployeePerformanceAnalysisService>();
                services.AddScoped<IPartSuggestionService, MockPartSuggestionService>();
            }

            // Other Infrastructure Services
            services.AddScoped<ITokenService, JwtService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITwoFactorService, TwoFactorService>();
            services.AddScoped<IErrorMessageService, ErrorMessageService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsService, NetgsmSmsService>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFCMNotificationService, FCMNotificationService>();
            services.AddScoped<IPaymentService, IyzicoPaymentService>();
            services.AddScoped<IMobilePaymentService, IyzicoMobilePaymentService>();
            services.AddScoped<IStockAlertService, StockAlertService>();
            services.AddScoped<IAutoOrderService, AutoOrderService>();
            services.AddScoped<IPdfInvoiceService, PdfInvoiceService>();
            services.AddScoped<MagicCarRepairAISupported.Application.Common.Services.WorkOrder.IPdfWorkOrderService, Infrastructure.Services.WorkOrder.PdfWorkOrderService>();
            services.AddScoped<IQrCodeService, QrCodeService>();
            services.AddScoped<Application.Common.Services.Barcode.IBarcodeService, Infrastructure.Services.Barcode.BarcodeService>();
            services.AddScoped<IExcelExportService, ExcelExportService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddScoped<IRedisCacheService, RedisService>();
            services.AddScoped<IRedisLockService>(sp =>
            {
                var connectionString = configuration["Redis:ConnectionString"] ?? "localhost:6379";
                return new RedisLockService(connectionString);
            });
            services.AddScoped<ICacheInvalidationService, CacheInvalidationService>();
            services.AddScoped<MagicCarRepairAISupported.Application.Common.Services.Audit.IAuditLogService, Infrastructure.Services.Audit.AuditLogService>();
            services.AddScoped<Application.Common.Services.Subscription.ISubscriptionService, Infrastructure.Services.Subscription.SubscriptionService>();
            services.AddScoped<Application.Common.Services.Commission.ICommissionService, Infrastructure.Services.Commission.CommissionService>();

            // Note: Middleware should NOT be registered in DI container
            // They are used directly in the middleware pipeline via UseMiddleware<T>() or custom extension methods

            return services;
        }
    }
}

