using ByteBuy.Core.DTO.Address;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserAddRequest(
    [Required] Guid RoleId,
    [Required] Guid CountryId,
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [MaxLength(15)] string? PhoneNumber,
    [Required] AddressAddRequest Address,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
    );

