using ByteBuy.Core.DTO.Public.AddressValueObj;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Public.Employee;

public record EmployeeAddressUpdateRequest(
    [Required] HomeAddressDto HomeAddress,
    [Required, MaxLength(15)] string PhoneNumber
    );
