using ByteBuy.Core.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class CardPaymentDetailsConfig : IEntityTypeConfiguration<CardPaymentDetails>
{
    public void Configure(EntityTypeBuilder<CardPaymentDetails> builder)
    {
        builder.Property(c => c.MaskedCardNumber).HasMaxLength(19);
        builder.Property(c => c.CardHolderName).HasMaxLength(30);
    }
}
