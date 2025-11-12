using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICompanyInfoService, CompanyInfoService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        return services;
    }
}
