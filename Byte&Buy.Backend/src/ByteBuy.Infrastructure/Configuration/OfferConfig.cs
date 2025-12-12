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

        builder.HasQueryFilter(item => item.IsActive);
    }
}
