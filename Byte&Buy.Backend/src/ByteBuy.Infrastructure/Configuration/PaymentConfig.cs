using ByteBuy.Core.Domain.Payments;
using ByteBuy.Core.Domain.Payments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {

        builder.Property(p => p.Status)
           .HasConversion<string>()
           .HasMaxLength(20)
           .IsRequired();

        builder.Property(p => p.Method)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(p => p.PaymentDetails)
            .WithOne(pd => pd.Payment)
            .HasForeignKey<PaymentDetails>(pd => pd.PaymentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(p => p.Amount, m =>
        {
            m.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();
            m.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}
