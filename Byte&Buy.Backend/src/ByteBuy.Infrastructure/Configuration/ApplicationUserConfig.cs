using ByteBuy.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);

        builder.HasMany(u => u.Items)
            .WithOne(o => o.CreatedBy)
            .HasForeignKey(o => o.CreatedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(item => item.IsActive);
    }
}
