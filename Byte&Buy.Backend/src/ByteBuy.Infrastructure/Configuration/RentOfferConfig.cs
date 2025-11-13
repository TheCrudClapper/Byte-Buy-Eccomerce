using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class RentOfferConfig : IEntityTypeConfiguration<RentOffer>
{
    public void Configure(EntityTypeBuilder<RentOffer> builder)
    {
        builder.OwnsOne(ro => ro.PricePerDay, ro =>
        {
             ro.Property(prop => prop.Currency).HasMaxLength(3);
             ro.Property(prop => prop.Amount).HasPrecision(18, 3);
        });

    }
}
