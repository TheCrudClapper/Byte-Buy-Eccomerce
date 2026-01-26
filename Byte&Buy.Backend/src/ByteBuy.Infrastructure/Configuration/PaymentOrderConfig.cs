using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class PaymentOrderConfig : IEntityTypeConfiguration<PaymentOrder>
{
    public void Configure(EntityTypeBuilder<PaymentOrder> builder)
    {
        builder.HasOne(po => po.OrderGroup)
            .WithMany(o => o.PaymentOrders)
            .HasForeignKey(po => po.OrderGroupId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(po => po.Payment)
            .WithMany(o => o.PaymentOrders)
            .HasForeignKey(po => po.PaymentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(item => item.IsActive);
    }
}
