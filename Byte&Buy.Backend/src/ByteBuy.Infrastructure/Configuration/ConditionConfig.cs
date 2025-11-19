using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class ConditionConfig : IEntityTypeConfiguration<Condition>
{
    public void Configure(EntityTypeBuilder<Condition> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(50);

        builder.HasQueryFilter(item => item.IsActive);
    }
}
