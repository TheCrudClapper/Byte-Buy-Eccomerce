using ByteBuy.Infrastructure.HttpClients.Company;
using ByteBuy.Infrastructure.HttpClients.Me;
using ByteBuy.Infrastructure.HttpClients.Public;
using ByteBuy.Infrastructure.Stores;
using ByteBuy.Services.Handlers;
using ByteBuy.Services.InfraContracts.HttpClients.Company;
using ByteBuy.Services.InfraContracts.HttpClients.Me;
using ByteBuy.Services.InfraContracts.HttpClients.Public;
using ByteBuy.Services.InfraContracts.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        //AddAsync Http Clients
        services.AddHttpClient<IAuthHttpClient, AuthHttpClient>();

        services.AddHttpClient<ICompanyEmployeeHttpClient, CompanyEmployeeHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyPermissionHttpClient, CompanyPermissionHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IUserPasswordHttpClient, UserPasswordsHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyRoleHttpClient, CompanyRolesHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyInfoHttpClient, CompanyHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyPortalUserHttpClient, CompanyPortalUserHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyCountryHttpClient, CompanyCountryHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyDeliveryHttpClient, CompanyDeliveriesHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyConditionHttpClient, CompanyConditionHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyCategoryHttpClient, CompanyCategoriesHttpClient>()
          .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyItemHttpClient, CompanyItemsHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<IImagePreviewHttpClient, ImagePreviewHttpClient>(options =>
        {
            options.BaseAddress = new Uri("http://localhost:5099/Images/");
        }).AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyDeliveryCarrierHttpClient, CompanyDeliveryCarriersHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanySaleOfferHttpClient, CompanySaleOfferHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyRentOfferHttpClient, CompanyRentOffersHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyUserHomeAddressHttpClient, CompanyUserHomeAddressHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyOrderHttpClient, CompanyOrdersHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyRentalHttpClient, CompanyRentalHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyStatisticsHttpClient, CompanyStatisticsHttpClient>()
            .AddHttpMessageHandler<BearerTokenHandler>();

        services.AddHttpClient<ICompanyDocumentsHttpClient, CompanyDocumentsHttpClient>()
           .AddHttpMessageHandler<BearerTokenHandler>();

        //AddAsync Public Http Clients
        services.AddHttpClient<IPublicCategoriesHttpClient, PublicCategoriesHttpClient>();
        services.AddHttpClient<IPublicConditionsHttpClients, PublicConditionsHttpClient>();
        services.AddHttpClient<IPublicCountriesHttpClient, PublicCountriesHttpClient>();
        services.AddHttpClient<IPublicDeliveriesHttpClient, PublicDeliveriesHttpClient>();

        //AddAsync Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        return services;
    }
}