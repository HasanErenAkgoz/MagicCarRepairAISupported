using System.Net.Http.Json;
using MagicCarRepairAISupported.WebAPI.Tests.Infrastructure;

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
}
