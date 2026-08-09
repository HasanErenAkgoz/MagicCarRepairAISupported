using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MagicCarRepairAISupported.WebAPI.Tests.Infrastructure;

public static class JwtTestTokenFactory
{
    public const string TestSecurityKey = "TestSigningKeyForIntegrationTests_MustBe32Chars!!";
    public const string Issuer = "MagicCarRepair";
    public const string Audience = "MagicCarRepair.API";

    public static string SecurityKey => Environment.GetEnvironmentVariable("TokenOptions__SecurityKey") ?? TestSecurityKey;
    public static string TokenIssuer => Environment.GetEnvironmentVariable("TokenOptions__Issuer") ?? Issuer;
    public static string TokenAudience => Environment.GetEnvironmentVariable("TokenOptions__Audience") ?? Audience;

    public static string CreateToken(int userType, int clientId = 1, int userId = 1)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, $"test-user-{userType}@test.local"),
            new Claim("UserType", userType.ToString()),
            new Claim("ClientId", clientId.ToString()),
            new Claim("Language", "tr"),
        };

        var token = new JwtSecurityToken(
            TokenIssuer,
            TokenAudience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
