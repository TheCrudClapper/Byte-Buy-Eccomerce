using ByteBuy.Core.Domain.DomainServices;
using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        //Add Application Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ICompanyInfoService, CompanyInfoService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IConditionService, ConditionService>();
        services.AddScoped<IDeliveryService, DeliveryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IPortalUserService, PortalUserService>();

        //Add Domain Services
        services.AddScoped<IAddressValidationService, AddressValidationService>();
        return services;
    }
}
