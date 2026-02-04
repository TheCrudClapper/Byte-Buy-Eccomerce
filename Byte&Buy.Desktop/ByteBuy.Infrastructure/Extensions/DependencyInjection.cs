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

        services.AddHttpClient<IPortalUserHttpClient, PortalUserHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICountryHttpClient, CountryHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IDeliveryHttpClient, DeliveryHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IConditionHttpClient, ConditionHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICategoryHttpClient, CategoryHttpClient>()
          .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IItemHttpClient, ItemHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IImagePreviewHttpClient, ImagePreviewHttpClient>(options =>
        {
            options.BaseAddress = new Uri("http://localhost:5099/Images/");
        }).AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IDeliveryCarrierHttpClient, DeliveryCarrierHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ISaleOfferHttpClient, SaleOfferHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IRentOfferHttpClient, RentOfferHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IAddressHttpClient, AddressHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IOrderHttpClient, OrderHttpClient>()
         .AddHttpMessageHandler<BearerTokenHandler>();

        //Add Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}