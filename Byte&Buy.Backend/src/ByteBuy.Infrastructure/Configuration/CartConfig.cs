using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasOne(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(c => c.UserId);

        builder.OwnsOne(c => c.TotalItemsValue, c =>
        {
            c.Property(prop => prop.Amount).HasColumnName("TotalItemsValue_Amount").HasPrecision(18, 3).IsRequired();
            c.Property(prop => prop.Currency).HasColumnName("TotalItemsValue_Currency").HasMaxLength(3).IsRequired();
        });

        builder.OwnsOne(c => c.TotalCartValue, c =>
        {
            c.Property(prop => prop.Amount).HasColumnName("TotalCartValue_Amount").HasPrecision(18, 3).IsRequired();
            c.Property(prop => prop.Currency).HasColumnName("TotalCartValue_Currency").HasMaxLength(3).IsRequired();
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}
