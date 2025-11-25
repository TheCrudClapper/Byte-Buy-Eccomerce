using ByteBuy.Services.Handlers;
using ByteBuy.Services.HttpClients.Abstractions;
using ByteBuy.Services.HttpClients.Implementations;
using ByteBuy.Services.ServiceContracts;
using ByteBuy.Services.Services;
using ByteBuy.Services.Stores.Abstractions;
using ByteBuy.Services.Stores.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Services.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        //Add Http Clients
        services.AddHttpClient<IAuthHttpClient, AuthHttpClient>();

        services.AddHttpClient<IEmployeeHttpClient, EmployeeHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>(); 

        services.AddHttpClient<IPermissionHttpClient, PermissionHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IUserHttpClient, UserHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();
        
        services.AddHttpClient<IRoleHttpClient, RoleHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();
        
        //Add Services
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IEmployeeService, EmployeeService>();
        services.AddSingleton<IRoleService, RoleService>();
        services.AddSingleton<IPermissionService, PermissionService>();
        
        //Add Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}