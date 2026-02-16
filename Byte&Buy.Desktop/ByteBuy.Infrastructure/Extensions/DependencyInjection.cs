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

        services.AddHttpClient<IRoleHttpClient, CompanyRolesHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyInfoHttpClient, CompanyHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IPortalUserHttpClient, CompanyPortalUserHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICountryHttpClient, CountryHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IDeliveryHttpClient, CompanyDeliveriesHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IConditionHttpClient, ConditionHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICategoryHttpClient, CompanyCategoriesHttpClient>()
          .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IItemHttpClient, CompanyItemsHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IImagePreviewHttpClient, ImagePreviewHttpClient>(options =>
        {
            options.BaseAddress = new Uri("http://localhost:5099/Images/");
        }).AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IDeliveryCarrierHttpClient, CompanyDeliveryCarriersHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ISaleOfferHttpClient, CompanySaleOfferHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IRentOfferHttpClient, CompanyRentOffersHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyUserHomeAddressHttpClient, CompanyUserHomeAddressHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IOrderHttpClient, CompanyOrdersHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IRentalHttpClient, CompanyRentalHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IStatisticsHttpClient, CompanyStatisticsHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IDocumentsHttpClient, DocumentsHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        //Add Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}