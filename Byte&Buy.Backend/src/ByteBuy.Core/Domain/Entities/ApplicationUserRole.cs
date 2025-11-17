using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public ApplicationUser User { get; set; } = null!;
    public ApplicationRole Role { get; set; } = null!;
}
