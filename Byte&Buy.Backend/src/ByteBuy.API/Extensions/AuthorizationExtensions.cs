using ByteBuy.API.Authorization;

namespace ByteBuy.API.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection LoadAuthorizationPolicies(this IServiceCollection services)
    {
        return services.AddAuthorization(options =>
        {
            var permissions = new[] { "companyinfo:update", "users:write", "products:edit", "orders:view", "employee:create", "employee:update" };
            foreach (var perm in permissions)
            {
                options.AddPolicy(perm, policy => policy.Requirements.Add(new PermissionRequirement(perm)));
            }
        });
    }
}
