using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Infrastructure.Services.FileUpload;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Filters;
using MagicCarRepairAISupported.Persistence.Repositories;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
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
            
            // Generic repositories for entities
            services.AddScoped(typeof(IEntityRepository<Vehicle, int>), typeof(EfEntityRepository<Vehicle, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Customer, int>), typeof(EfEntityRepository<Customer, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Employee, int>), typeof(EfEntityRepository<Employee, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<Part, int>), typeof(EfEntityRepository<Part, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<PartStock, int>), typeof(EfEntityRepository<PartStock, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<PartSupplier, int>), typeof(EfEntityRepository<PartSupplier, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<WorkOrderItem, int>), typeof(EfEntityRepository<WorkOrderItem, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<WorkOrderLabor, int>), typeof(EfEntityRepository<WorkOrderLabor, BaseDbContext>));
            services.AddScoped(typeof(IEntityRepository<WorkOrderPhoto, int>), typeof(EfEntityRepository<WorkOrderPhoto, BaseDbContext>));
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
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();

            });
           
            services.AddIdentityCoreService(configuration);
            services.AddSwaggerServices(configuration);
            services.AddHostedService<Persistence.Startup.HostedServices.PermissionInitializerHostedService>();
            services.AddHostedService<Persistence.Startup.HostedServices.StockAlertMonitoringHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.AppointmentReminderHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.InsuranceReminderHostedService>();
            services.AddHostedService<Infrastructure.Startup.HostedServices.InvoiceDueDateReminderHostedService>();
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

    }
}
