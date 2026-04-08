using ByteBuy.Core.Contracts;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.Domain.RepositoryContracts.UoW;
using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.ServiceContracts;
using ByteBuy.Infrastructure.DbContexts;
using ByteBuy.Infrastructure.HangfireJobs;
using ByteBuy.Infrastructure.Repositories;
using ByteBuy.Infrastructure.Repositories.UnitOfWork;
using ByteBuy.Infrastructure.ServiceContracts;
using ByteBuy.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBuy.Infrastructure.Extensions;

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
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IConditionRepository, ConditionRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPortalUserRepository, PortalUserRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IDeliveryCarrierRepository, DeliveryCarrierRepository>();
        services.AddScoped<IImageStorage, ImageStorage>();
        services.AddScoped<ISaleOfferRepository, SaleOfferRepository>();
        services.AddScoped<IRentOfferRepository, RentOfferRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOfferRepository, OfferRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        services.AddScoped<IPdfGenerator<OrderDetailsPdfModel>, QuestOrderDetailsPdfGenerator>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IAddressReadRepository, AddressReadRepository>();


        //Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Hangfire Services
        services.AddScoped<IRentalStatusService, RentalStatusService>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();

        //Hangfire Jobs
        services.AddScoped<RentalStatusJob>();
        services.AddScoped<OrderStatusJob>();

        return services;
    }
}
