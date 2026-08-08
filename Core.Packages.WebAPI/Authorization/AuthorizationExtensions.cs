using Microsoft.AspNetCore.Authorization;

namespace MagicCarRepairAISupported.WebAPI.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddMagicCarRepairAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicyNames.ShopStaff, policy =>
                policy.RequireAssertion(ctx => HasUserType(ctx.User, 1, 2, 3)));

            options.AddPolicy(AuthPolicyNames.CustomerOnly, policy =>
                policy.RequireAssertion(ctx => HasUserType(ctx.User, 4)));

            options.AddPolicy(AuthPolicyNames.CustomerOrSystemAdmin, policy =>
                policy.RequireAssertion(ctx => HasUserType(ctx.User, 1, 4)));

            options.AddPolicy(AuthPolicyNames.SystemAdminOnly, policy =>
                policy.RequireAssertion(ctx => HasUserType(ctx.User, 1)));
        });

        return services;
    }

    private static bool HasUserType(System.Security.Claims.ClaimsPrincipal user, params int[] allowed)
    {
        var claim = user.FindFirst("UserType")?.Value;
        if (string.IsNullOrEmpty(claim) || !int.TryParse(claim, out var userType))
            return false;
        return allowed.Contains(userType);
    }
}
