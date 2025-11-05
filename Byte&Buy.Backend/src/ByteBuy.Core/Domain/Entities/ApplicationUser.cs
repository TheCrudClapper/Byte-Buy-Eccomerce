using ByteBuy.Core.Domain.EntityContracts;
using Microsoft.AspNetCore.Identity;

namespace ByteBuy.Core.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, ISoftDelete
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public DateTime? DateDeleted { get; set; }
}
