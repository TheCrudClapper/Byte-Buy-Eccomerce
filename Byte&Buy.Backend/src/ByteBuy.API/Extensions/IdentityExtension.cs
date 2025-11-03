using ByteBuy.Core.Domain.Entities;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ByteBuy.API.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<User, Role, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<Role, ApplicationDbContext, Guid>>();

        return services;
    }
}
