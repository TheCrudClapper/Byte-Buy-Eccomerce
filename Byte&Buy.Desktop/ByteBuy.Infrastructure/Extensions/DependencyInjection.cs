using ByteBuy.Infrastructure.HttpClients;
using ByteBuy.Infrastructure.Stores;
using ByteBuy.Services.Handlers;
using ByteBuy.Services.InfraContracts.HttpClients;
using ByteBuy.Services.InfraContracts.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
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

        services.AddHttpClient<ICompanyInfoHttpClient, CompanyInfoHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        //Add Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}