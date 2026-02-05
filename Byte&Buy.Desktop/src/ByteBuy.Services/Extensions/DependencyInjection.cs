using ByteBuy.Services.ServiceContracts;
using ByteBuy.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Services.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        //Add Services
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IEmployeeService, EmployeeService>();
        services.AddSingleton<IRoleService, RoleService>();
        services.AddSingleton<ICompanyInfoService, CompanyInfoService>();
        services.AddSingleton<IPermissionService, PermissionService>();
        services.AddSingleton<IPortalUserService, PortalUserService>();
        services.AddSingleton<ICountryService, CountryService>();
        services.AddSingleton<IDeliveryService, DeliveryService>();
        services.AddSingleton<IConditionService, ConditionService>();
        services.AddSingleton<ICategoryService, CategoryService>();
        services.AddSingleton<IItemService, ItemService>();
        services.AddSingleton<IRentOfferService, RentOfferService>();
        services.AddSingleton<ISaleOfferService, SaleOfferService>();
        services.AddSingleton<IImageService, ImageService>();
        services.AddSingleton<IDeliveryCarrierService, DeliveryCarrierService>();
        services.AddSingleton<ISaleOfferService, SaleOfferService>();
        services.AddSingleton<IRentOfferService, RentOfferService>();
        services.AddSingleton<IHomeAddressService, HomeAddressService>();
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<IRentalService, RentalService>();

        return services;
    }
}