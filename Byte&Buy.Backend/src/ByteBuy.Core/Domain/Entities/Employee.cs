using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;

namespace ByteBuy.Core.Domain.Entities;

public sealed class Employee : ApplicationUser
{
    public AddressValueObj HomeAddress { get; private set; } = null!;
    private Employee(string firstName, string lastName, string email)
        : base(firstName, lastName, email) { }

    public static Result<Employee> Create(string firstName,
        string lastName,
        string email,
        string street,
        string houseNumber,
        string postalCode,
        string city,
        string country,
        string? flatNumber)
    {

        var addressResult = AddressValueObj
            .Create(street, houseNumber, postalCode, city, country, flatNumber);

        if (addressResult.IsFailure)
            return Result.Failure<Employee>(addressResult.Error);

        var employee = new Employee(firstName, lastName, email);

        employee.HomeAddress = addressResult.Value;

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

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
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
        string? flatNumber)
    {
        var addressResult = AddressValueObj.Create(
            street, houseNumber, postalCode, city, country, flatNumber);

        if (addressResult.IsFailure)
            return Result.Failure(addressResult.Error);

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HomeAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }

}

