using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Permissions.Commands.ScanAndRegister;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Seeds;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Persistence.Startup.HostedServices
{
    public class PermissionInitializerHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PermissionInitializerHostedService> _logger;

        public PermissionInitializerHostedService(IServiceProvider serviceProvider, ILogger<PermissionInitializerHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();

                    if (!await context.Database.CanConnectAsync(cancellationToken))
                    {
                        _logger.LogError("Failed to connect to database. Please check your connection string and ensure the database exists.");
                        return;
                    }

                    if (context.Database.IsRelational())
                    {
                        await context.Database.MigrateAsync(cancellationToken);
                    }

                    await SeedRolesAsync(context, roleManager, cancellationToken);

                    var clientIds = await context.Clients.AsNoTracking()
                        .Select(c => c.Id)
                        .ToListAsync(cancellationToken);

                    await mediator.Send(
                        new ScanAndRegisterPermissionsCommand { PermissionsOnly = true },
                        cancellationToken);

                    foreach (var clientId in clientIds)
                    {
                        tenantService.SetCurrentClientId(clientId);
                        await mediator.Send(
                            new ScanAndRegisterPermissionsCommand
                            {
                                ClientId = clientId,
                                SkipBootstrapUser = true
                            },
                            cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing permissions and roles: {Message}", ex.Message);
                // Don't throw - allow application to start even if initialization fails
            }
        }

        private async Task SeedRolesAsync(BaseDbContext context, RoleManager<Role> roleManager, CancellationToken cancellationToken)
        {
            try
            {
                var existingRoles = await context.Roles.ToListAsync(cancellationToken);
                var demoRoles = RolePermissionSeedData.GetDemoClientRoles();
                var testRoles = RolePermissionSeedData.GetTestClientRoles();

                var clientIds = await context.Clients.AsNoTracking().Select(c => c.Id).ToHashSetAsync(cancellationToken);

                foreach (var seedRole in demoRoles.Concat(testRoles))
                {
                    if (!clientIds.Contains(seedRole.ClientId))
                    {
                        _logger.LogDebug("Skipping role {RoleName}: ClientId {ClientId} not in database yet", seedRole.Name, seedRole.ClientId);
                        continue;
                    }

                    var existing = existingRoles.FirstOrDefault(r => r.Name == seedRole.Name && r.ClientId == seedRole.ClientId);
                    if (existing == null)
                    {
                        var role = new Role
                        {
                            Name = seedRole.Name,
                            NormalizedName = seedRole.NormalizedName,
                            ClientId = seedRole.ClientId,
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        };
                        var result = await roleManager.CreateAsync(role);
                        if (!result.Succeeded)
                        {
                            _logger.LogWarning("Failed to create role {RoleName} for ClientId {ClientId}: {Errors}", 
                                seedRole.Name, seedRole.ClientId, string.Join(", ", result.Errors.Select(e => e.Description)));
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error seeding roles: {Message}", ex.Message);
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

