using ByteBuy.Core.DTO.AddressValueObj;
using System.ComponentModel.DataAnnotations;

namespace ByteBuy.Core.DTO.Employee;

public record EmployeeAddressUpdateRequest(
    [Required] HomeAddressDto HomeAddress,
    [Required, MaxLength(15)] string PhoneNumber
    );
