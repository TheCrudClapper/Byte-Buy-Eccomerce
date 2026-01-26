using ByteBuy.Core.Domain.Entities;
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

        builder.OwnsOne(p => p.Amount, m =>
        {
            m.Property(prop => prop.Amount).HasPrecision(18, 3).IsRequired();
            m.Property(prop => prop.Currency).HasMaxLength(3).IsRequired();
        });

        builder.HasQueryFilter(item => item.IsActive);
    }
}
