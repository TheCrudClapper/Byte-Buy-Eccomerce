using ByteBuy.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<SaleOffer> SaleOffers { get; set; }
    public DbSet<RentOffer> RentOffers { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Condition> Condition { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartOffer> CartOffers { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Delivery> Delivery { get; set; }
    public DbSet<OfferDelivery> OfferDelivery { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public ApplicationDbContext(){ }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //Scan current assembly for entities confi and apply
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        builder.Entity<Offer>().ToTable("Offers");
        builder.Entity<SaleOffer>().ToTable("SaleOffers");
        builder.Entity<RentOffer>().ToTable("RentOffers");
    }
}
