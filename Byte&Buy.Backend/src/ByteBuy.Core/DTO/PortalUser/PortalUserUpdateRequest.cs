using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBuy.Core.DTO.PortalUser;

public record PortalUserUpdateRequest(
    [Required] Guid RoleId,
    [Required] Guid CountryId,
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    string? Password,
    [MaxLength(15)] string? PhoneNumber,
    [Required, MaxLength(50)] string Street,
    [Required, MaxLength(10)] string HouseNumber,
    [Required, MaxLength(20)] string PostalCode,
    [Required, MaxLength(50)] string City,
    [Required, MaxLength(50)] string Country,
    [MaxLength(10)] string? FlatNumber,
    IEnumerable<Guid>? GrantedPermissionIds,
    IEnumerable<Guid>? RevokedPermissionIds
    );



