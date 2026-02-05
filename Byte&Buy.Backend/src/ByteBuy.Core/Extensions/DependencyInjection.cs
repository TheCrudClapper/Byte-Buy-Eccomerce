using ByteBuy.Core.Domain.DomainServices;
using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        //AddUserShippingAddressAsync Application Services
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
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<IDeliveryCarrierService, DeliveryCarrierService>();
        services.AddScoped<ISaleOfferService, SaleOfferService>();
        services.AddScoped<IRentOfferService, RentOfferService>();
        services.AddScoped<IOfferReadService, OfferReadService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IUserSaleOfferService, UserSaleOfferService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IItemHelperService, ItemHelperService>();
        services.AddScoped<IUserRentOfferService, UserRentOfferService>();
        services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IOrderCreateService, OrderCreateService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRentalService, RentalService>();

        //AddUserShippingAddressAsync Domain Services
        services.AddScoped<IAddressValidationService, AddressValidationService>();
        return services;
    }
}
