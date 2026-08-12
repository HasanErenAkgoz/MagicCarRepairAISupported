using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MagicCarRepairAISupported.WebAPI.Tests.Infrastructure;

public class MagicCarRepairWebApplicationFactory : WebApplicationFactory<Program>
{
    // Program validates JWT settings while its service graph is being built,
    // before ConfigureAppConfiguration runs for minimal-host test factories.
    // Set process configuration during type initialization so that validation and
    // the generated test tokens always use the same deterministic values.
    static MagicCarRepairWebApplicationFactory()
    {
        Environment.SetEnvironmentVariable("TokenOptions__SecurityKey", JwtTestTokenFactory.SecurityKey);
        Environment.SetEnvironmentVariable("TokenOptions__Issuer", JwtTestTokenFactory.TokenIssuer);
        Environment.SetEnvironmentVariable("TokenOptions__Audience", JwtTestTokenFactory.TokenAudience);
        Environment.SetEnvironmentVariable("TokenOptions__AccessTokenExpiration", "60");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureAppConfiguration((_, config) =>
        {
            var settings = new Dictionary<string, string?>
            {
                // This in-memory provider is registered last for the test host.
                // Explicit CI env values are honored; otherwise deterministic test
                // values always satisfy the production JWT validation contract.
                ["TokenOptions:SecurityKey"] = JwtTestTokenFactory.SecurityKey,
                ["TokenOptions:Issuer"] = JwtTestTokenFactory.TokenIssuer,
                ["TokenOptions:Audience"] = JwtTestTokenFactory.TokenAudience,
                ["TokenOptions:AccessTokenExpiration"] = "60",
                ["AIOptions:Provider"] = "Mock",
                ["SuperPassword:Enabled"] = "false",
            };

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                settings["ConnectionStrings:DefaultConnection"] = connectionString;
            }

            config.AddInMemoryCollection(settings);
        });

        builder.ConfigureTestServices(services =>
        {
            var hosted = services.Where(d =>
                d.ServiceType == typeof(IHostedService) &&
                d.ImplementationType != null &&
                (d.ImplementationType.Namespace?.Contains("HostedService", StringComparison.Ordinal) == true ||
                 d.ImplementationType.Name.Contains("DatabaseSeed", StringComparison.Ordinal) ||
                 d.ImplementationType.Name.Contains("PermissionInitializer", StringComparison.Ordinal)))
                .ToList();

            foreach (var descriptor in hosted)
            {
                services.Remove(descriptor);
            }
        });
    }

    public HttpClient CreateAuthenticatedClient(int userType, int clientId = 1, int userId = 1)
    {
        var client = CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                JwtTestTokenFactory.CreateToken(userType, clientId, userId));
        return client;
    }
}
