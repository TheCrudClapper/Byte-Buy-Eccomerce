using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class ConditionConfig : IEntityTypeConfiguration<Condition>
{
    public void Configure(EntityTypeBuilder<Condition> builder)
    {
        builder.HasQueryFilter(item => item.IsActive);
    }
}
