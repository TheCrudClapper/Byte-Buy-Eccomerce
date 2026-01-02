using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OfferConfig : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasOne(o => o.CreatedBy)
            .WithMany(u => u.Offers)
            .HasForeignKey(o => o.CreatedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(e => e.OwnerAddressSnapshot, a =>
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
