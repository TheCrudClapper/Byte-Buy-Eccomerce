using ByteBuy.Core.Domain.Roles;
using ByteBuy.Core.Domain.Users.Base;

namespace ByteBuy.Core.Domain.Users.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public ApplicationUser User { get; set; } = null!;
    public ApplicationRole Role { get; set; } = null!;
}
