using ByteBuy.Core.Domain.Carts;
using ByteBuy.Core.Domain.Carts.Entities;
using ByteBuy.Core.Domain.Categories;
using ByteBuy.Core.Domain.Companies;
using ByteBuy.Core.Domain.Conditions;
using ByteBuy.Core.Domain.Countries;
using ByteBuy.Core.Domain.Deliveries;
using ByteBuy.Core.Domain.DeliveryCarriers;
using ByteBuy.Core.Domain.Items;
using ByteBuy.Core.Domain.Items.Entities;
using ByteBuy.Core.Domain.Offers;
using ByteBuy.Core.Domain.Offers.Base;
using ByteBuy.Core.Domain.Offers.Entities;
using ByteBuy.Core.Domain.Orders;
using ByteBuy.Core.Domain.Orders.Entities;
using ByteBuy.Core.Domain.Payments;
using ByteBuy.Core.Domain.Payments.Entities;
using ByteBuy.Core.Domain.Payments.Enums;
using ByteBuy.Core.Domain.Permissions;
using ByteBuy.Core.Domain.Rentals;
using ByteBuy.Core.Domain.Roles;
using ByteBuy.Core.Domain.Roles.Entities;
using ByteBuy.Core.Domain.Users;
using ByteBuy.Core.Domain.Users.Base;
using ByteBuy.Core.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ByteBuy.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid,
    IdentityUserClaim<Guid>,
    ApplicationUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>

{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<SaleOffer> SaleOffers { get; set; }
    public DbSet<RentOffer> RentOffers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<ShippingAddress> ShippingAddresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartOffer> CartOffers { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<OfferDelivery> OfferDeliveries { get; set; }
    public DbSet<OrderDelivery> OrderDeliveries { get; set; }
    public DbSet<RentCartOffer> RentCartOffers { get; set; }
    public DbSet<SaleCartOffer> SaleCartOffers { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<PortalUser> PortalUsers { get; set; }
    public DbSet<DeliveryCarrier> DeliveryCarriers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentOrder> PaymentOrders { get; set; }
    public DbSet<Rental> Rentals { get; set; }


    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public ApplicationDbContext() { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Scan current assembly for entities config and apply
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        //TPT Mapping
        builder.Entity<Offer>().ToTable("Offers");
        builder.Entity<SaleOffer>().ToTable("SaleOffers");
        builder.Entity<RentOffer>().ToTable("RentOffers");

        builder.Entity<CartOffer>().ToTable("CartOffers");
        builder.Entity<SaleCartOffer>().ToTable("SaleCartOffers");
        builder.Entity<RentCartOffer>().ToTable("RentCartOffers");

        //TPH Mapping
        builder.Entity<ApplicationUser>()
           .HasDiscriminator<string>("UserType")
           .HasValue<PortalUser>("PortalUser")
           .HasValue<Employee>("Employee");

        builder.Entity<OrderLine>()
            .HasDiscriminator<string>("OrderLineType")
            .HasValue<RentOrderLine>("RentOrderLine")
            .HasValue<SaleOrderLine>("SaleOrderLine");

        builder.Entity<PaymentDetails>()
            .HasDiscriminator(p => p.Method)
            .HasValue<CardPaymentDetails>(PaymentMethod.Card)
            .HasValue<BlikPaymentDetails>(PaymentMethod.Blik);
    }
}
