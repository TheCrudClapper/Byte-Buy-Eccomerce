using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class OrderGroupConfig : IEntityTypeConfiguration<OrderGroup>
{
    public void Configure(EntityTypeBuilder<OrderGroup> builder)
    {
        builder.HasOne(og => og.Buyer)
            .WithMany()
            .HasForeignKey(og => og.BuyerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(og => og.TotalPrice, tp =>
        {
            tp.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
            tp.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();
        });

        builder.HasMany(og => og.Orders)
            .WithOne()
            .HasForeignKey("OrderGroupId");

        builder.Property(prop => prop.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.ToTable("OrderGroups");

    }
}