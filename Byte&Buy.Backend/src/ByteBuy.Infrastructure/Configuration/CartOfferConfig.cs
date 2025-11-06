using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class CartOfferConfig : IEntityTypeConfiguration<CartOffer>
{
    public void Configure(EntityTypeBuilder<CartOffer> builder)
    {
        builder.HasOne(co => co.Offer)
            .WithMany(o => o.CartOffers)
            .HasForeignKey(co => co.OfferId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(co => co.Cart)
            .WithMany(o => o.CartOffers)
            .HasForeignKey(co => co.CartId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}
