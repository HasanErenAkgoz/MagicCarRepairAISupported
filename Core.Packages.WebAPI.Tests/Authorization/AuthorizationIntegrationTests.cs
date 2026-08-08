using System.Net.Http.Json;
using MagicCarRepairAISupported.WebAPI.Tests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MagicCarRepairAISupported.WebAPI.Tests.Authorization;

public class AuthorizationIntegrationTests : IClassFixture<MagicCarRepairWebApplicationFactory>
{
    private readonly MagicCarRepairWebApplicationFactory _factory;

    public AuthorizationIntegrationTests(MagicCarRepairWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Customer_GetCustomers_Returns403()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 4);
        var response = await client.GetAsync("/api/Customers");
        Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task ShopManager_GetCustomers_DoesNotReturn403()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 2);
        var response = await client.GetAsync("/api/Customers");
        Assert.NotEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Customer_GetOpenQuoteRequests_Returns403()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 4);
        var response = await client.GetAsync("/api/quote-requests/open");
        Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task ShopManager_GetOpenQuoteRequests_DoesNotReturn403()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 2);
        var response = await client.GetAsync("/api/quote-requests/open");
        Assert.NotEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Customer_CustomerPortalWorkOrders_DoesNotReturn403()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 4);
        var response = await client.GetAsync("/api/customer-portal/work-orders");
        Assert.NotEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Customer_CustomerMediaEndpoint_IsAllowedByPolicy()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 4);
        var response = await client.GetAsync("/api/customer-media/customers/1/work-orders/1/photos/1");
        Assert.NotEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Anonymous_DiagnosisAssetUpload_IsRejected()
    {
        using var client = _factory.CreateClient();
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent([1, 2, 3]), "file", "damage.jpg");
        var response = await client.PostAsync("/api/AI/diagnosis-assets", content);
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task SystemAdmin_CustomerMediaEndpoint_IsAllowedByPolicy()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 1);
        var response = await client.GetAsync("/api/customer-media/customers/1/work-orders/1/photos/1");
        Assert.NotEqual(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Manager_ImpersonateUser_Returns403()
    {
        using var client = _factory.CreateAuthenticatedClient(userType: 2);
        var response = await client.PostAsJsonAsync(
            "/api/Auth/impersonate",
            new { TargetUserId = 1 });
        Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task Health_ReturnsHealthy()
    {
        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/health");
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Production_Uploads_AreNotAvailableFromAnonymousStaticFiles()
    {
        using var productionFactory = _factory.WithWebHostBuilder(builder => builder.UseEnvironment("Production"));
        using var client = productionFactory.CreateClient();

        var response = await client.GetAsync("/uploads/Test/R34-Walpaper.jpg");

        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Production_AnalyzeDamagePhotos_RejectsAnonymousRawUrlInput()
    {
        using var productionFactory = _factory.WithWebHostBuilder(builder => builder.UseEnvironment("Production"));
        using var client = productionFactory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/AI/analyze-damage-photos", new
        {
            photoPaths = new[] { "https://example.invalid/private", "/etc/passwd" }
        });

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Production_AnalyzeDamagePhotos_ReturnsGoneForAuthenticatedCaller()
    {
        using var productionFactory = _factory.WithWebHostBuilder(builder => builder.UseEnvironment("Production"));
        using var client = productionFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer", JwtTestTokenFactory.CreateToken(userType: 4, clientId: 1));

        var response = await client.PostAsJsonAsync("/api/AI/analyze-damage-photos", new
        {
            photoPaths = new[] { "https://example.invalid/private" }
        });

        Assert.Equal(System.Net.HttpStatusCode.Gone, response.StatusCode);
    }

    [Fact]
    public async Task FileDownload_RequiresAuthentication()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/Files/download?fileName=R34-Walpaper.jpg&containerName=Test");

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Production_LegacyUploadFlag_ExplicitlyEnablesStaticFilesForMigration()
    {
        var fixtureRoot = Path.Combine(Path.GetTempPath(), $"magic-car-repair-static-{Guid.NewGuid():N}");
        var fixtureFile = Path.Combine(fixtureRoot, "uploads", "Test", "migration-fixture.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(fixtureFile)!);
        await File.WriteAllTextAsync(fixtureFile, "migration fixture");

        try
        {
            using var productionFactory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Production");
                builder.UseWebRoot(fixtureRoot);
                builder.ConfigureAppConfiguration((_, config) =>
                    config.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["Media:EnableLegacyAnonymousStaticFiles"] = "true"
                    }));
            });
            using var client = productionFactory.CreateClient();

            var response = await client.GetAsync("/uploads/Test/migration-fixture.txt");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        finally
        {
            Directory.Delete(fixtureRoot, recursive: true);
        }
    }
}
