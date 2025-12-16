using ByteBuy.Core.Contracts;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.Repositories;
using ByteBuy.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly("ByteBuy.Infrastructure"));
        });

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICompanyInfoRepository, CompanyInfoRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IConditionRepository, ConditionRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPortalUserRepository, PortalUserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IImageStorage, ImageStorage>();
        return services;
    }
}
