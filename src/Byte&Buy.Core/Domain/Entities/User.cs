using Byte_Buy.Core.Domain.EntityContracts;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Byte_Buy.Core.Domain.Entities;

public class User : IdentityUser<Guid>, ISoftDelete
{
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(50)]
    public string LastName { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public DateTime? DateDeleted { get; set; }
}
