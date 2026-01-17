using MagicCarRepairAISupported.Application;
using MagicCarRepairAISupported.Infrastructure;
using MagicCarRepairAISupported.Persistence;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Middlewares;
using MagicCarRepairAISupported.WebAPI.Extensions;
using MagicCarRepairAISupported.WebAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});


ConfigureServices(builder);
var app = builder.Build();
ConfigureMiddleware(app);
app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
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

}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();
    app.UseCors("AllowAllOrigins");

    app.UseHttpsRedirection();
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




