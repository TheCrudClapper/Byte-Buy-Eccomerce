using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class Employee : ApplicationUser
{
    private Employee(string firstName, string lastName, string email, string? phoneNumber)
        : base(firstName, lastName, email, phoneNumber) { }

    public static Result<Employee> Create(string firstName,
        string lastName,
        string email,
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        string? phoneNumber,
        IEnumerable<Guid>? revokedPermissions,
        IEnumerable<Guid>? grantedPermissions,
        IAddressValidationService validator)
    {
        var validatioResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validatioResult.IsFailure)
            return Result.Failure<Employee>(validatioResult.Error);

        var addressResult = AddressValueObject
            .Create(street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);

        if (addressResult.IsFailure)
            return Result.Failure<Employee>(addressResult.Error);

        var employee = new Employee(firstName, lastName, email, phoneNumber);

        employee.HomeAddress = addressResult.Value;

        employee.AssignPermissionsToUser(revokedPermissions ?? [], grantedPermissions ?? []);

        return employee;
    }

    public Result ChangeHomeAddress(
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        string? phoneNumber,
        IAddressValidationService validator)
    {
        var addressResult = AddressValueObject.Create(
            street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        var phoneNumberResult = ChangePhoneNumber(phoneNumber);
        if (phoneNumberResult.IsFailure)
            return Result.Failure(phoneNumberResult.Error);

        HomeAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public Result Update(
        string firstName,
        string lastName,
        string email,
        string street,
        string houseNumber,
        string postalCity,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        string? phoneNumber,
        IEnumerable<Guid>? grantedPermissionIds,
        IEnumerable<Guid>? revokedPermissionIds,
        IAddressValidationService validator)
    {

        var validatioResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validatioResult.IsFailure)
            return Result.Failure(validatioResult.Error);

        var addressResult = AddressValueObject.Create(
            street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        HomeAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;

        var permissionOverrideResult = SetPermissionOverrides(revokedPermissionIds, grantedPermissionIds);
        if (permissionOverrideResult.IsFailure)
            return Result.Failure(permissionOverrideResult.Error);

        return Result.Success();
    }
}

