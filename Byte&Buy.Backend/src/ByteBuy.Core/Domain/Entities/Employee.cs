using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

public sealed class Employee : ApplicationUser
{
    public AddressValueObj HomeAddress { get; private set; } = null!;
    private Employee(string firstName, string lastName, string email, string? phoneNumber)
        : base(firstName, lastName, email, phoneNumber) { }

    public static Result<Employee> Create(string firstName,
        string lastName,
        string email,
        string street,
        string houseNumber,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        string? phoneNumber,
        IEnumerable<Guid> revokedPermissions,
        IEnumerable<Guid> grantedPermissions)
    {
        var validatioResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validatioResult.IsFailure)
            return Result.Failure<Employee>(validatioResult.Error);

        var addressResult = AddressValueObj
            .Create(street, houseNumber, postalCode, city, country, flatNumber);

        if (addressResult.IsFailure)
            return Result.Failure<Employee>(addressResult.Error);

        var employee = new Employee(firstName, lastName, email, phoneNumber);

        employee.HomeAddress = addressResult.Value;

        employee.AssignPermissionsToUser(revokedPermissions, grantedPermissions);

        return employee;
    }

    public Result ChangeAddress(
        string street,
        string houseNumber,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        string? phoneNumber)
    {
        var addressResult = AddressValueObj.Create(
            street, houseNumber, postalCode, city, country, flatNumber);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        var phoneNumberResult = ChangePhoneNumber(phoneNumber);
        if (phoneNumberResult.IsFailure)
            return Result.Failure(phoneNumberResult.Error);

        HomeAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;
        return Result.Success();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        var suffix = "_DELETED_" + Guid.NewGuid();
        NormalizedUserName += suffix.ToUpper();
        IsActive = false;
        DateDeleted = DateTime.UtcNow;
        DeactivateAllUserPermissions();
    }

    public Result Update(
        string firstName,
        string lastName,
        string email,
        string street,
        string houseNumber,
        string postalCode,
        string city,
        string country,
        string? flatNumber,
        string? phoneNumber)
    {

        var validatioResult = ValidateBasicInfo(firstName, lastName, email, phoneNumber);
        if (validatioResult.IsFailure)
            return Result.Failure(validatioResult.Error);

        var addressResult = AddressValueObj.Create(
            street, houseNumber, postalCode, city, country, flatNumber);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        HomeAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

}

