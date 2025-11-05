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
    }
}
