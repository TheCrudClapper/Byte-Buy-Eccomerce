using ByteBuy.Core.Domain.Roles;
using ByteBuy.Core.Domain.Users.Base;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.API.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
}
