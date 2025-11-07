using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;
/// <summary>
/// Class that configures keys, relationships in Address entity
/// </summary>
public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(a => a.Country)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
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

    }
}
