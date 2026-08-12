using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.WebAPI.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MagicCarRepairAISupported.WebAPI.Tests.Authorization;

[Collection("Participant authorization")]
public sealed class ChatAttachmentAuthorizationIntegrationTests : IClassFixture<MagicCarRepairWebApplicationFactory>
{
    private const int ClientId = 81101;
    private const int OtherClientId = 81102;
    private const int WorkOrderId = 81111;
    private const int ManagerId = 81121;
    private const int SenderId = 81122;
    private const int ReceiverId = 81123;
    private readonly MagicCarRepairWebApplicationFactory _factory;

    public ChatAttachmentAuthorizationIntegrationTests(MagicCarRepairWebApplicationFactory factory) => _factory = factory;

    [Fact]
    public async Task Draft_owner_can_bind_once_and_only_work_order_participants_can_download()
    {
        await SeedAsync();
        using var manager = _factory.CreateAuthenticatedClient((int)UserType.Manager, ClientId, ManagerId);
        using var sender = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, SenderId);
        using var receiver = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, ReceiverId);
        using var foreign = _factory.CreateAuthenticatedClient((int)UserType.Employee, OtherClientId, SenderId);
        Assert.Equal(HttpStatusCode.OK, (await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = SenderId })).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = ReceiverId })).StatusCode);

        var attachmentId = await UploadAsync(sender, "report.pdf", "application/pdf", [37, 80, 68, 70]);
        var stolenBind = await receiver.PostAsJsonAsync("/api/Chat/send", new { receiverId = SenderId, workOrderId = WorkOrderId, message = "stolen", attachmentIds = new[] { attachmentId } });
        Assert.Equal(HttpStatusCode.Forbidden, stolenBind.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await sender.GetAsync($"/api/Chat/workorder/{WorkOrderId}/attachments/{attachmentId}/download")).StatusCode);

        var bind = await sender.PostAsJsonAsync("/api/Chat/send", new { receiverId = ReceiverId, workOrderId = WorkOrderId, message = "document", attachmentIds = new[] { attachmentId } });
        Assert.Equal(HttpStatusCode.OK, bind.StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await receiver.GetAsync($"/api/Chat/workorder/{WorkOrderId}/attachments/{attachmentId}/download")).StatusCode);
        // A foreign tenant must not learn that this work order or attachment exists.
        Assert.Equal(HttpStatusCode.NotFound, (await foreign.GetAsync($"/api/Chat/workorder/{WorkOrderId}/attachments/{attachmentId}/download")).StatusCode);
        Assert.Equal(HttpStatusCode.Forbidden, (await sender.PostAsJsonAsync("/api/Chat/send", new { receiverId = ReceiverId, workOrderId = WorkOrderId, message = "rebind", attachmentIds = new[] { attachmentId } })).StatusCode);
    }

    [Fact]
    public async Task Upload_rejects_non_participants_and_disallowed_content_types()
    {
        await SeedAsync();
        using var manager = _factory.CreateAuthenticatedClient((int)UserType.Manager, ClientId, ManagerId);
        using var sender = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, SenderId);
        Assert.Equal(HttpStatusCode.Forbidden, (await UploadResponseAsync(sender, "payload.exe", "application/octet-stream", [1, 2])).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = SenderId })).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, (await UploadResponseAsync(sender, "payload.exe", "application/octet-stream", [1, 2])).StatusCode);
    }

    [Fact]
    public async Task Anonymous_expired_and_invalid_storage_key_access_is_rejected()
    {
        await SeedAsync();
        using var anonymous = _factory.CreateClient();
        Assert.Equal(HttpStatusCode.Unauthorized, (await UploadResponseAsync(anonymous, "report.pdf", "application/pdf", [37, 80, 68, 70])).StatusCode);

        using var manager = _factory.CreateAuthenticatedClient((int)UserType.Manager, ClientId, ManagerId);
        using var sender = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, SenderId);
        using var receiver = _factory.CreateAuthenticatedClient((int)UserType.Employee, ClientId, ReceiverId);
        Assert.Equal(HttpStatusCode.OK, (await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = SenderId })).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await manager.PostAsJsonAsync($"/api/Chat/workorder/{WorkOrderId}/participants", new { userId = ReceiverId })).StatusCode);

        var expiredId = await UploadAsync(sender, "expired.pdf", "application/pdf", [37, 80, 68, 70]);
        await ExpireAttachmentAsync(expiredId);
        Assert.Equal(HttpStatusCode.Forbidden, (await sender.PostAsJsonAsync("/api/Chat/send", new { receiverId = ReceiverId, workOrderId = WorkOrderId, message = "expired", attachmentIds = new[] { expiredId } })).StatusCode);

        var traversalId = await UploadAsync(sender, "safe.pdf", "application/pdf", [37, 80, 68, 70]);
        Assert.Equal(HttpStatusCode.OK, (await sender.PostAsJsonAsync("/api/Chat/send", new { receiverId = ReceiverId, workOrderId = WorkOrderId, message = "safe", attachmentIds = new[] { traversalId } })).StatusCode);
        await SetInvalidStorageKeyAsync(traversalId);
        Assert.Equal(HttpStatusCode.NotFound, (await receiver.GetAsync($"/api/Chat/workorder/{WorkOrderId}/attachments/{traversalId}/download")).StatusCode);
    }

    private async Task<int> UploadAsync(HttpClient client, string fileName, string contentType, byte[] bytes)
    {
        var response = await UploadResponseAsync(client, fileName, contentType, bytes);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.GetProperty("attachmentId").GetInt32();
    }

    private async Task<HttpResponseMessage> UploadResponseAsync(HttpClient client, string fileName, string contentType, byte[] bytes)
    {
        using var form = new MultipartFormDataContent();
        var file = new ByteArrayContent(bytes);
        file.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        form.Add(file, "file", fileName);
        return await client.PostAsync($"/api/Chat/workorder/{WorkOrderId}/attachments", form);
    }

    private async Task SeedAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        await db.Database.MigrateAsync();
        if (await db.WorkOrders.IgnoreQueryFilters().AnyAsync(x => x.Id == WorkOrderId))
        {
            await db.WorkOrderParticipants.IgnoreQueryFilters().Where(x => x.WorkOrderId == WorkOrderId).ExecuteDeleteAsync();
            await db.ChatAttachments.IgnoreQueryFilters().Where(x => x.WorkOrderId == WorkOrderId).ExecuteDeleteAsync();
            await db.ChatMessages.IgnoreQueryFilters().Where(x => x.WorkOrderId == WorkOrderId).ExecuteDeleteAsync();
            return;
        }

        db.Clients.AddRange(new Client { Id = ClientId, Name = "Chat attachment QA", Code = "chat-attachment-qa" }, new Client { Id = OtherClientId, Name = "Chat attachment other", Code = "chat-attachment-other" });
        db.Users.AddRange(User(ManagerId, ClientId, UserType.Manager), User(SenderId, ClientId, UserType.Employee), User(ReceiverId, ClientId, UserType.Employee));
        db.Customers.Add(new Customer { Id = 81131, ClientId = ClientId, IdentityNo = "81100000001", FirstName = "QA", LastName = "Customer", Email = "chat-qa@example.test", PhoneNumber = "5558110001", Address = "Test" });
        db.Employees.AddRange(Employee(SenderId, ClientId), Employee(ReceiverId, ClientId));
        db.Vehicles.Add(new Vehicle { Id = 81141, ClientId = ClientId, CustomerId = 81131, LicensePlate = "34QA8111", Brand = "Test", Model = "Car", Color = "Blue" });
        db.WorkOrders.Add(new WorkOrder { Id = WorkOrderId, ClientId = ClientId, WorkOrderNumber = "QA-CHAT-ATTACHMENT", CustomerId = 81131, VehicleId = 81141, Status = WorkOrderStatus.InProgress });
        await db.SaveChangesAsync();
    }

    private async Task ExpireAttachmentAsync(int attachmentId)
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        await db.ChatAttachments.IgnoreQueryFilters().Where(x => x.Id == attachmentId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.ExpiresAt, DateTime.UtcNow.AddDays(-1)));
    }

    private async Task SetInvalidStorageKeyAsync(int attachmentId)
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
        await db.ChatAttachments.IgnoreQueryFilters().Where(x => x.Id == attachmentId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.StorageKey, "private-media/chat-drafts/../outside.pdf"));
    }

    private static User User(int id, int clientId, UserType type) => new() { Id = id, ClientId = clientId, UserType = type, UserName = $"chat-{id}-{clientId}", NormalizedUserName = $"CHAT-{id}-{clientId}", Email = $"chat-{id}-{clientId}@example.test", NormalizedEmail = $"CHAT-{id}-{clientId}@EXAMPLE.TEST", FirstName = "QA", LastName = "User", PasswordHash = "not-used" };
    private static Employee Employee(int userId, int clientId) => new() { Id = userId + 100, UserId = userId, ClientId = clientId, EmployeeNo = $"QA-{userId}", FirstName = "QA", LastName = "Employee", Phone = "5558110000", Email = $"employee-{userId}@example.test", Position = EmployeePosition.Mechanic, Salary = 1, HireDate = DateTime.UtcNow, EmploymentStatus = EmploymentStatus.Active };
}
