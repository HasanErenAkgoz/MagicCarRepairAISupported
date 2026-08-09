using System.Net;
using System.Net.Http.Json;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.WebAPI.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MagicCarRepairAISupported.WebAPI.Tests.Authorization;

/// <summary>Exercises the complete HTTP, claims, tenant and PostgreSQL authorization path.</summary>
[Collection("Participant authorization")]
public sealed class WorkOrderParticipantAuthorizationIntegrationTests : IClassFixture<MagicCarRepairWebApplicationFactory>
{
    private const int ClientId = 81001;
    private const int OtherClientId = 81002;
    private const int WorkOrderId = 81011;
    private const int ClosedWorkOrderId = 81012;
    private const int ManagerId = 81021;
    private const int EmployeeId = 81022;
    private const int AdvisorId = 81023;
    private const int InactiveAdvisorId = 81024;
    private readonly MagicCarRepairWebApplicationFactory _factory;

    public WorkOrderParticipantAuthorizationIntegrationTests(MagicCarRepairWebApplicationFactory factory) => _factory = factory;

    [Fact]
    public async Task Participant_management_enforces_tenant_role_and_terminal_status()
    {
        await SeedAsync();

        using var manager = _factory.CreateAuthenticatedClient((int)UserType.Manager, ClientId, ManagerId);
        var allowed = await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = AdvisorId });
        Assert.Equal(HttpStatusCode.OK, allowed.StatusCode);

        using var employee = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, EmployeeId);
        var nonManager = await employee.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = AdvisorId });
        Assert.Equal(HttpStatusCode.Forbidden, nonManager.StatusCode);

        using var foreignManager = _factory.CreateAuthenticatedClient((int)UserType.Manager, OtherClientId, ManagerId);
        var crossTenant = await foreignManager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = AdvisorId });
        Assert.Equal(HttpStatusCode.NotFound, crossTenant.StatusCode);

        var terminal = await manager.PostAsJsonAsync($"/api/Chat/workorder/{ClosedWorkOrderId}/participants", new { userId = AdvisorId });
        Assert.Equal(HttpStatusCode.BadRequest, terminal.StatusCode);
    }

    [Fact]
    public async Task Revoked_or_inactive_advisor_is_denied_on_the_next_chat_request()
    {
        await SeedAsync();

        using var manager = _factory.CreateAuthenticatedClient((int)UserType.Manager, ClientId, ManagerId);
        using var advisor = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, AdvisorId);
        var grant = await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = AdvisorId });
        Assert.Equal(HttpStatusCode.OK, grant.StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await advisor.GetAsync($"/api/Chat/workorder/{WorkOrderId}")).StatusCode);

        var revoke = await manager.DeleteAsync($"/api/Chat/workorder/{WorkOrderId}/participants/{AdvisorId}");
        Assert.Equal(HttpStatusCode.OK, revoke.StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, (await advisor.GetAsync($"/api/Chat/workorder/{WorkOrderId}")).StatusCode);

        await SetEmploymentStatusAsync(InactiveAdvisorId, EmploymentStatus.Active);
        var inactiveGrant = await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = InactiveAdvisorId });
        Assert.Equal(HttpStatusCode.OK, inactiveGrant.StatusCode);
        using var inactiveAdvisor = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, InactiveAdvisorId);
        Assert.Equal(HttpStatusCode.OK, (await inactiveAdvisor.GetAsync($"/api/Chat/workorder/{WorkOrderId}")).StatusCode);
        await SetEmploymentStatusAsync(InactiveAdvisorId, EmploymentStatus.Suspended);
        Assert.Equal(HttpStatusCode.Forbidden, (await inactiveAdvisor.GetAsync($"/api/Chat/workorder/{WorkOrderId}")).StatusCode);
    }

    private async Task SeedAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        await db.Database.MigrateAsync();
        if (await db.WorkOrders.IgnoreQueryFilters().AnyAsync(x => x.Id == WorkOrderId))
        {
            await db.WorkOrderParticipants.IgnoreQueryFilters()
                .Where(x => x.WorkOrderId == WorkOrderId)
                .ExecuteDeleteAsync();
            var inactiveEmployee = await db.Employees.IgnoreQueryFilters()
                .SingleAsync(x => x.UserId == InactiveAdvisorId && x.ClientId == ClientId);
            inactiveEmployee.EmploymentStatus = EmploymentStatus.Suspended;
            await db.SaveChangesAsync();
            return;
        }

        db.Clients.AddRange(new Client { Id = ClientId, Name = "Participant QA", Code = "participant-qa" }, new Client { Id = OtherClientId, Name = "Participant QA Other", Code = "participant-qa-other" });
        db.Users.AddRange(User(ManagerId, ClientId, UserType.Manager), User(EmployeeId, ClientId, UserType.Employee), User(AdvisorId, ClientId, UserType.Employee), User(InactiveAdvisorId, ClientId, UserType.Employee));
        db.Customers.Add(new Customer { Id = 81031, ClientId = ClientId, IdentityNo = "81000000001", FirstName = "QA", LastName = "Customer", Email = "qa-customer@example.test", PhoneNumber = "5558100001", Address = "Test" });
        db.Employees.AddRange(Employee(EmployeeId, ClientId, EmploymentStatus.Active), Employee(AdvisorId, ClientId, EmploymentStatus.Active), Employee(InactiveAdvisorId, ClientId, EmploymentStatus.Suspended));
        db.Vehicles.Add(new Vehicle { Id = 81041, ClientId = ClientId, CustomerId = 81031, LicensePlate = "34QA8101", Brand = "Test", Model = "Car", Color = "Blue" });
        db.WorkOrders.AddRange(new WorkOrder { Id = WorkOrderId, ClientId = ClientId, WorkOrderNumber = "QA-PARTICIPANT-OPEN", CustomerId = 81031, VehicleId = 81041, Status = WorkOrderStatus.InProgress }, new WorkOrder { Id = ClosedWorkOrderId, ClientId = ClientId, WorkOrderNumber = "QA-PARTICIPANT-CLOSED", CustomerId = 81031, VehicleId = 81041, Status = WorkOrderStatus.Delivered });
        await db.SaveChangesAsync();
    }

    private async Task SetEmploymentStatusAsync(int userId, EmploymentStatus status)
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        var employee = await db.Employees.IgnoreQueryFilters().SingleAsync(x => x.UserId == userId && x.ClientId == ClientId);
        employee.EmploymentStatus = status;
        await db.SaveChangesAsync();
    }

    private static User User(int id, int clientId, UserType type) => new() { Id = id, ClientId = clientId, UserType = type, UserName = $"qa-{id}", NormalizedUserName = $"QA-{id}", Email = $"qa-{id}@example.test", NormalizedEmail = $"QA-{id}@EXAMPLE.TEST", FirstName = "QA", LastName = "User", PasswordHash = "not-used" };
    private static Employee Employee(int userId, int clientId, EmploymentStatus status) => new() { Id = userId + 100, UserId = userId, ClientId = clientId, EmployeeNo = $"QA-{userId}", FirstName = "QA", LastName = "Employee", Phone = "5558100000", Email = $"employee-{userId}@example.test", Position = EmployeePosition.Mechanic, Salary = 1, HireDate = DateTime.UtcNow, EmploymentStatus = status };
}

[CollectionDefinition("Participant authorization", DisableParallelization = true)]
public sealed class ParticipantAuthorizationCollection;
