using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);

        builder.HasMany(u => u.Offers)
            .WithOne(o => o.CreatedBy)
            .HasForeignKey(o => o.CreatedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(e => e.HomeAddress, a =>
        {
            a.Property(p => p.City).HasMaxLength(50);
            a.Property(p => p.Street).HasMaxLength(50);
            a.Property(p => p.PostalCode).HasMaxLength(20);
            a.Property(p => p.PostalCity).HasMaxLength(50);
            a.Property(p => p.HouseNumber).HasMaxLength(10);
            a.Property(p => p.Country).HasMaxLength(50);
            a.Property(p => p.FlatNumber).HasMaxLength(10);
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}
