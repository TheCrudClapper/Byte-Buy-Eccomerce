using ByteBuy.Core.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class PaymentOrderConfig : IEntityTypeConfiguration<PaymentOrder>
{
    public void Configure(EntityTypeBuilder<PaymentOrder> builder)
    {
        builder.OwnsOne(po => po.Amount, po =>
        {
            po.Property(prop => prop.Currency).HasMaxLength(3);
            po.Property(prop => prop.Amount).HasPrecision(18, 3);
        });

        builder.HasOne(po => po.Payment)
            .WithMany(o => o.PaymentOrders)
            .HasForeignKey(po => po.PaymentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(item => item.IsActive);
    }
}
