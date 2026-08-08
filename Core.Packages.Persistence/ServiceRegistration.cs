using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Persistence.Services;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Infrastructure.Configurations.Token;
using MagicCarRepairAISupported.Infrastructure.Services.FileUpload;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Database;
using MagicCarRepairAISupported.Persistence.Filters;
using MagicCarRepairAISupported.Persistence.Repositories;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace MagicCarRepairAISupported.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCorePersistenceServices<TContext>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IShopTrustService, ShopTrustService>();
            services.AddScoped<IPermissionRepository, PermissionRepositoriy>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IUploadedFileRepository, UploadedFileRepository>();
            
            // Multi-tenant repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IErrorMessageRepository, ErrorMessageRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<IPartStockRepository, PartStockRepository>();
            services.AddScoped<IPartSupplierRepository, PartSupplierRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository>();
            services.AddScoped<IQuoteRequestRepository, QuoteRequestRepository>();
            services.AddScoped<IQuoteResponseRepository, QuoteResponseRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserDeviceTokenRepository, UserDeviceTokenRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            // Tenant-aware customer reads (IgnoreQueryFilters + ClientId); do not use generic EfEntityRepository for Customer.
            services.AddScoped<IEntityRepository<Customer, int>>(sp =>
                sp.GetRequiredService<ICustomerRepository>());
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<ISalaryPaymentRepository, SalaryPaymentRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
            services.AddScoped<IInsurancePolicyRepository, InsurancePolicyRepository>();
            services.AddScoped<IInsuranceClaimRepository, InsuranceClaimRepository>();
            services.AddScoped<IServiceRatingRepository, ServiceRatingRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IServicePortfolioRepository, ServicePortfolioRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<IFacilityPhotoRepository, FacilityPhotoRepository>();
            services.AddScoped<ILoyaltyPointRepository, LoyaltyPointRepository>();
            services.AddScoped<IRewardRepository, RewardRepository>();
            services.AddScoped<IHelpArticleRepository, HelpArticleRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IPasswordResetOtpRepository, PasswordResetOtpRepository>();
            services.AddScoped<IUserDeviceRepository, UserDeviceRepository>();
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            
            // Generic repositories for entities
            services.AddScoped(typeof(IEntityRepository<Vehicle, int>), typeof(EfEntityRepository<Vehicle, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Employee, int>), typeof(EfEntityRepository<Employee, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Part, int>), typeof(EfEntityRepository<Part, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<PartStock, int>), typeof(EfEntityRepository<PartStock, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<PartSupplier, int>), typeof(EfEntityRepository<PartSupplier, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<WorkOrderItem, int>), typeof(EfEntityRepository<WorkOrderItem, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<WorkOrderLabor, int>), typeof(EfEntityRepository<WorkOrderLabor, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<WorkOrderPhoto, int>), typeof(EfEntityRepository<WorkOrderPhoto, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<VehiclePhoto, int>), typeof(EfEntityRepository<VehiclePhoto, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<PartPhoto, int>), typeof(EfEntityRepository<PartPhoto, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<QuoteRequestPhoto, int>), typeof(EfEntityRepository<QuoteRequestPhoto, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<StockAlert, int>), typeof(EfEntityRepository<StockAlert, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<AutoOrder, int>), typeof(EfEntityRepository<AutoOrder, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<NotificationTemplate, int>), typeof(EfEntityRepository<NotificationTemplate, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Income, int>), typeof(EfEntityRepository<Income, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Expense, int>), typeof(EfEntityRepository<Expense, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<SalaryPayment, int>), typeof(EfEntityRepository<SalaryPayment, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Tax, int>), typeof(EfEntityRepository<Tax, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Invoice, int>), typeof(EfEntityRepository<Invoice, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<InvoiceItem, int>), typeof(EfEntityRepository<InvoiceItem, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Appointment, int>), typeof(EfEntityRepository<Appointment, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Payment, int>), typeof(EfEntityRepository<Payment, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<InsuranceCompany, int>), typeof(EfEntityRepository<InsuranceCompany, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<InsurancePolicy, int>), typeof(EfEntityRepository<InsurancePolicy, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<InsuranceClaim, int>), typeof(EfEntityRepository<InsuranceClaim, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<ServiceRating, int>), typeof(EfEntityRepository<ServiceRating, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<AuditLog, int>), typeof(EfEntityRepository<AuditLog, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<ChatMessage, int>), typeof(EfEntityRepository<ChatMessage, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Subscription, int>), typeof(EfEntityRepository<Subscription, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<SubscriptionPayment, int>), typeof(EfEntityRepository<SubscriptionPayment, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Reminder, int>), typeof(EfEntityRepository<Reminder, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Commission, int>), typeof(EfEntityRepository<Commission, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<PasswordResetOtp, int>), typeof(EfEntityRepository<PasswordResetOtp, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<UserDevice, int>), typeof(EfEntityRepository<UserDevice, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<UserSession, int>), typeof(EfEntityRepository<UserSession, BaseDbContext>));
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddDbContext<TContext>(options =>
            {
                options.UseMagicCarRepairDatabase(configuration);
                options.AddInterceptors(new Interceptors.RequiredStringSaveInterceptor());
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });
           
            services.AddIdentityCoreService(configuration);
            services.AddJwtAuthentication(configuration);
            services.AddSwaggerServices(configuration);
            // DatabaseSeed must run first: roles reference Clients (FK_Roles_Clients_ClientId).
            services.AddHostedService<Persistence.Startup.HostedServices.DatabaseSeedHostedService>();
            services.AddHostedService<Persistence.Startup.HostedServices.PermissionInitializerHostedService>();
            services.AddHostedService<Persistence.Startup.HostedServices.StockAlertMonitoringHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.AppointmentReminderHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.InsuranceReminderHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.InvoiceDueDateReminderHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.SubscriptionExpirationHostedService>();
            services.AddDataProtection();
            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MagicCarRepairAI API",
                    Version = "v1",
                    Description = "Oto Servis Yönetim Sistemi API Dokümantasyonu"
                });

                // Aynı sınıf adı farklı namespace'lerde olduğunda (örn. iki AddWorkOrderPhotoCommand) schemaId çakışmasını önler
                c.CustomSchemaIds(type => type.FullName!.Replace("+", ".", StringComparison.Ordinal));
                
                // IFormFile için özel schema mapping
                c.MapType<IFormFile>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });
                
                // File upload document filter ekle (en önce çalışmalı)
                c.DocumentFilter<SwaggerFileUploadDocumentFilter>();
                
                // File upload parameter filter ekle (parametreleri filtrelemek için)
                c.ParameterFilter<SwaggerFileUploadParameterFilter>();
                
                // File upload operation filter ekle (RequestBody oluşturmak için)
                c.OperationFilter<SwaggerFileUploadOperationFilter>();
                
                // JWT Authentication için Swagger konfigürasyonu
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddIdentityCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(5);
            })
               .AddRoles<Role>()
               .AddEntityFrameworkStores<BaseDbContext>()
               .AddDefaultTokenProviders()
               .AddSignInManager()
               .AddUserManager<UserManager<User>>()
               .AddRoleManager<RoleManager<Role>>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection("TokenOptions").Get<Infrastructure.Configurations.Token.TokenOptions>();
            
            if (tokenOptions == null || string.IsNullOrEmpty(tokenOptions.SecurityKey))
            {
                throw new InvalidOperationException("TokenOptions configuration is missing or invalid.");
            }

            if (tokenOptions.SecurityKey.Length < 32)
            {
                throw new InvalidOperationException("Security key must be at least 256 bits (32 characters) long.");
            }

            // IOptions<TokenOptions> (e.g. TokenTestController); disambiguate from Microsoft.AspNetCore.Identity.TokenOptions
            services.Configure<MagicCarRepairAISupported.Infrastructure.Configurations.Token.TokenOptions>(configuration.GetSection("TokenOptions"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = securityKey,
                    ClockSkew = TimeSpan.Zero // Token expiration'ı tam olarak kontrol et
                };

                // Events for debugging
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception != null)
                        {
                            // Log authentication failures for debugging
                            Console.WriteLine($"JWT Authentication Failed: {context.Exception.Message}");
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        // Token validated successfully
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

    }
}
