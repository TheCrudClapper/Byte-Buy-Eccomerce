using ByteBuy.Core.Domain.Payments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class BlikPaymentDetailsConfig : IEntityTypeConfiguration<BlikPaymentDetails>
{
    public void Configure(EntityTypeBuilder<BlikPaymentDetails> builder)
    {
        builder.Property(b => b.PhoneNumber).HasMaxLength(15);
    }
}
