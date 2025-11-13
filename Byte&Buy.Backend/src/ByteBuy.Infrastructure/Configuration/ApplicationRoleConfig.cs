using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasQueryFilter(item => item.IsActive);
    }
}
