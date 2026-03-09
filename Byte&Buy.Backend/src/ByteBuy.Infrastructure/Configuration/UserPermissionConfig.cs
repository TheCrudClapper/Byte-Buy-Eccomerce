using ByteBuy.Core.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteBuy.Infrastructure.Configuration;

public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(up => up.Permission)
            .WithMany(u => u.UserPermissions)
            .HasForeignKey(u => u.PermissionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("UserPermissions");

        builder.HasQueryFilter(item => item.IsActive);
    }
}
