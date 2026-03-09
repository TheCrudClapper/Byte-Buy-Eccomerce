using ByteBuy.Core.Domain.Offers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class SaleOfferConfig : IEntityTypeConfiguration<SaleOffer>
{
    public void Configure(EntityTypeBuilder<SaleOffer> builder)
    {
        builder.OwnsOne(so => so.PricePerItem, so =>
         {
             so.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
             so.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();
         });

    }
}
