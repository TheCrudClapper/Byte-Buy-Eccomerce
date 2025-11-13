using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.RentOrderItem)
               .WithOne(roi => roi.Rental)
               .HasForeignKey<Rental>(r => r.RentOrderItemId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(item => item.IsActive);
    }
}