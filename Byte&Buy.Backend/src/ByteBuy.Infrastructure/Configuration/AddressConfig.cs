using ByteBuy.Core.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;
/// <summary>
/// Class that configures keys, relationships in ShippingAddress entity
/// </summary>
public class AddressConfig : IEntityTypeConfiguration<ShippingAddress>
{
    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.ToTable("ShippingAddresses");

        builder.HasOne(a => a.Country)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.User)
            .WithMany(u => u.ShippingAddresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(a => a.Label)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.City)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Street)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.HouseNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.PostalCity)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.PostalCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(a => a.FlatNumber)
            .HasMaxLength(10);

        builder.HasQueryFilter(item => item.IsActive);
    }
}
