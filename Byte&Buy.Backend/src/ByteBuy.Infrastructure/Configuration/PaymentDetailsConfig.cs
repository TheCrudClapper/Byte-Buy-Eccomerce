
using ByteBuy.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class PaymentDetailsConfig : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
    {
        builder.HasQueryFilter(pd => pd.IsActive);
    }
}
