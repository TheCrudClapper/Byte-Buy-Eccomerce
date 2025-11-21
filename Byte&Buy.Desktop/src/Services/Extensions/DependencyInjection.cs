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
        services.AddHttpClient<IAuthHttpClient, AuthHttpClient>(client 
            => client.BaseAddress = new Uri("http://localhost:5099"));
        
        services.AddHttpClient<IEmployeeHttpClient, EmployeeHttpClient>(client 
            => client.BaseAddress = new Uri("http://localhost:5099"))
            .AddHttpMessageHandler<BearerTokenHandler>(); 
        
        //Add Services
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IEmployeeService, EmployeeService>();
        
        //Add Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}