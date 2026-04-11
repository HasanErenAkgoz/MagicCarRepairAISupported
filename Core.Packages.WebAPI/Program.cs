using MagicCarRepairAISupported.Application;
using MagicCarRepairAISupported.Infrastructure;
using MagicCarRepairAISupported.Persistence;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Middlewares;
using MagicCarRepairAISupported.Persistence.Seeds;
using MagicCarRepairAISupported.WebAPI.Extensions;
using MagicCarRepairAISupported.WebAPI.Hubs;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Development: allow any origin (mobile emulator, local browser, etc.)
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithExposedHeaders("*");
        }
        else
        {
            // Production: restrict to configured origins only
            var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
    });
});


builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("auth", opt =>
    {
        opt.PermitLimit = 10;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0;
    });
    options.RejectionStatusCode = 429;
});

ConfigureServices(builder);
var app = builder.Build();

// ── Startup seed: Araç fotoğrafları yoksa DB'ye ekle ─────────────────────
await VehiclePhotoDataSeeder.SeedAsync(app.Services);

ConfigureMiddleware(app);
app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers(mvcOptions =>
        {
            // .NET 9: non-nullable reference type property'ler için implicit [Required] eklenmesini engelle
            // Bu olmadan string LicensePlate, string Brand vb. alanlar body binding validation'a takılıyor
            mvcOptions.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new MagicCarRepairAISupported.Domain.Converters.UserTypeJsonConverter());
            // .NET 9: nullable annotation strict check'i kapat — non-nullable string property'lerin
            // JSON'da null gelmesi durumunda JsonException fırlatmasını önler
            options.JsonSerializerOptions.RespectNullableAnnotations = false;
        });

    // [ApiController] model validation hatasını bizim formatımıza çevir
    builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            // Body parse hatası tespiti: parametre adı "command"/"query" VEYA JSON exception içeren hata
            var isBodyParseError =
                context.ModelState.ContainsKey("command") ||
                context.ModelState.ContainsKey("query") ||
                context.ModelState.Values.Any(v => v.Errors.Any(e =>
                    e.Exception != null ||
                    e.ErrorMessage.Contains("JSON", StringComparison.OrdinalIgnoreCase) ||
                    e.ErrorMessage.Contains("convert", StringComparison.OrdinalIgnoreCase)));

            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Any() == true)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new
            {
                success = false,
                errorCode = isBodyParseError ? "BODY_PARSE_ERROR" : "VALIDATION_ERROR",
                message = isBodyParseError ? "BODY_PARSE_ERROR" : "VALIDATION_ERROR",
                errors
            });
        };
    });
    builder.Services.AddHttpContextAccessor();
    
    // Swagger Configuration - AddEndpointsApiExplorer ekle (AddSwaggerServices içinde değil)
    builder.Services.AddEndpointsApiExplorer();
    
    // SignalR
    builder.Services.AddSignalR();
    builder.Services.AddSignalRServices();
    
    builder.Services.AddCoreApplicationServices();
    builder.Services.AddCoreInfrastructureServices(builder.Configuration);
    // AddSwaggerServices burada çağrılıyor (AddCorePersistenceServices içinde)
    builder.Services.AddCorePersistenceServices<BaseDbContext>(builder.Configuration);
    
    // Background Jobs
    builder.Services.AddHostedService<MagicCarRepairAISupported.Infrastructure.Jobs.ReminderJob>();

}

void ConfigureMiddleware(WebApplication app)
{
    // CORS'u EN BAŞTA kullan (tüm middleware'lerden önce)
    app.UseCors("AllowAllOrigins");

    app.UseRateLimiter();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();
    
    // Development'ta HTTP isteklerine izin ver (HTTPS redirection'ı devre dışı bırak)
    // Production'da HTTPS redirection aktif olacak
    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }
    
    app.UseRouting(); 

    app.UseCustomMiddlewares();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<NotificationHub>("/hubs/notifications");
        endpoints.MapHub<WorkOrderHub>("/hubs/workorders");
        endpoints.MapHub<ChatHub>("/hubs/chat");
        endpoints.MapHub<DashboardHub>("/hubs/dashboard");
    });
}




