using ByteBuy.Infrastructure.Handlers;
using ByteBuy.Infrastructure.HttpClients.Company;
using ByteBuy.Infrastructure.HttpClients.Factories;
using ByteBuy.Infrastructure.HttpClients.Me;
using ByteBuy.Infrastructure.HttpClients.Public;
using ByteBuy.Infrastructure.Stores;
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
        services.AddAuthHeaderHandler();

        services.AddHttpClient("Backend");

        services.AddHttpClient("BackendAuthorized")
            .AddHttpMessageHandler<BearerTokenHandler>();

        //Factory for HTTP Clients
        services.AddSingleton<IBackendHttpClientFactory, BackendHttpClientFactory>();

        services.AddSingleton<IAuthHttpClient, AuthHttpClient>();
        services.AddSingleton<ICompanyEmployeeHttpClient, CompanyEmployeeHttpClient>();
        services.AddSingleton<ICompanyPermissionHttpClient, CompanyPermissionHttpClient>();
        services.AddSingleton<IUserPasswordHttpClient, UserPasswordsHttpClient>();
        services.AddSingleton<ICompanyRoleHttpClient, CompanyRolesHttpClient>();
        services.AddSingleton<ICompanyInfoHttpClient, CompanyHttpClient>();
        services.AddSingleton<ICompanyPortalUserHttpClient, CompanyPortalUserHttpClient>();
        services.AddSingleton<ICompanyCountryHttpClient, CompanyCountryHttpClient>();
        services.AddSingleton<ICompanyDeliveryHttpClient, CompanyDeliveriesHttpClient>();
        services.AddSingleton<ICompanyConditionHttpClient, CompanyConditionHttpClient>();
        services.AddSingleton<ICompanyCategoryHttpClient, CompanyCategoriesHttpClient>();
        services.AddSingleton<ICompanyItemHttpClient, CompanyItemsHttpClient>();
        services.AddSingleton<ICompanyDeliveryCarrierHttpClient, CompanyDeliveryCarriersHttpClient>();
        services.AddSingleton<ICompanySaleOfferHttpClient, CompanySaleOfferHttpClient>();
        services.AddSingleton<ICompanyRentOfferHttpClient, CompanyRentOffersHttpClient>();
        services.AddSingleton<ICompanyUserHomeAddressHttpClient, CompanyUserHomeAddressHttpClient>();
        services.AddSingleton<ICompanyOrderHttpClient, CompanyOrdersHttpClient>();
        services.AddSingleton<ICompanyRentalHttpClient, CompanyRentalHttpClient>();
        services.AddSingleton<ICompanyStatisticsHttpClient, CompanyStatisticsHttpClient>();
        services.AddSingleton<ICompanyDocumentsHttpClient, CompanyDocumentsHttpClient>();

        //Static Resource
        services.AddSingleton<IImagePreviewHttpClient, ImagePreviewHttpClient>();

        //Add Public Http Clients
        services.AddSingleton<IPublicCategoriesHttpClient, PublicCategoriesHttpClient>();
        services.AddSingleton<IPublicConditionsHttpClients, PublicConditionsHttpClient>();
        services.AddSingleton<IPublicCountriesHttpClient, PublicCountriesHttpClient>();
        services.AddSingleton<IPublicDeliveriesHttpClient, PublicDeliveriesHttpClient>();

        //Add Token Store
        services.AddSingleton<ITokenStore, TokenStore>();
        services.AddSingleton<IUserSettingsStore, UserSettingsStore>();
        return services;
    }
}