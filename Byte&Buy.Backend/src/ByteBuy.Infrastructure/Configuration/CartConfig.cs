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

        builder.OwnsOne(c => c.TotalItemsValue, c =>
        {
            c.Property(prop => prop.Amount).HasColumnName("TotalItemsValue_Amount").HasPrecision(18, 3);
            c.Property(prop => prop.Currency).HasColumnName("TotalItemsValue_Currency").HasMaxLength(3);
        });

        builder.OwnsOne(c => c.TotalCartValue, c => {
            c.Property(prop => prop.Amount).HasColumnName("TotalCartValue_Amount").HasPrecision(18, 3);
            c.Property(prop => prop.Currency).HasColumnName("TotalCartValue_Currency").HasMaxLength(3);
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}
