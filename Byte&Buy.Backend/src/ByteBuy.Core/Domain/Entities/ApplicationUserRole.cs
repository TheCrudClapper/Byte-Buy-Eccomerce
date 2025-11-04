using ByteBuy.Core.Domain.EntityContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>, ISoftDelete
{
    public ApplicationUser User { get; set; } = null!;
    public ApplicationRole Role { get; set; } = null!;
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
