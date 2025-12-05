using ByteBuy.Core.DTO.Address;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserUpdateRequest(
    [Required] Guid RoleId,
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    string? Password,
    [MaxLength(15)] string? PhoneNumber,
    AddressUpdateRequest? Address,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
    );



