using ByteBuy.Core.Domain.DomainServicesContracts;
using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

/// <summary>
/// Represents unique information about the ByteBuy company.
/// This entity cannot be deleted and only one instance exists in the system.
/// </summary>
public class Company : AuditableEntity
{
    public string CompanyName { get; private set; } = null!;
    public string TIN { get; private set; } = null!;
    public AddressValueObject CompanyAddress { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Slogan { get; private set; } = null!;

    //EF
    public ICollection<Employee> Employees { get; private set; } = [];

    private Company() { }

    private Company(
        string companyName,
        string tin,
        string email,
        string phone,
        string slogan,
        AddressValueObject address)
    {
        CompanyAddress = address;
        CompanyName = companyName;
        TIN = tin;
        Email = email;
        Phone = phone;
        Slogan = slogan;
    }

    private static Result ValidateBasicInfo(
       string companyName,
       string tin,
       string email,
       string phone,
       string slogan)
    {
        if (string.IsNullOrWhiteSpace(companyName) || companyName.Length > 50)
            return Result.Failure(CompanyInfoErrors.CompanyNameInvalid);

        if (string.IsNullOrWhiteSpace(tin) || tin.Length > 20)
            return Result.Failure(CompanyInfoErrors.TinInvalid);

        if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
            return Result.Failure(CompanyInfoErrors.EmailInvalid);

        if (string.IsNullOrWhiteSpace(phone) || phone.Length > 16)
            return Result.Failure(CompanyInfoErrors.PhoneInvalid);

        if (!string.IsNullOrWhiteSpace(slogan) && slogan.Length > 30)
            return Result.Failure(CompanyInfoErrors.SloganInvalid);

        return Result.Success();
    }

    public static Result<Company> Create(
    string companyName,
    string tin,
    string email,
    string phone,
    string slogan,
    string street,
    string houseNumber,
    string postalCity,
    string postalCode,
    string city,
    string country,
    string? flatNumber,
    IAddressValidationService validator)
    {
        var validationResult = ValidateBasicInfo(companyName, tin, email, phone, slogan);
        if (validationResult.IsFailure)
            return Result.Failure<Company>(validationResult.Error);

        var addressResult = AddressValueObject.Create(street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);
        if (addressResult.IsFailure)
            return Result.Failure<Company>(addressResult.Error);

        return new Company(companyName, tin, email, phone, slogan, addressResult.Value);
    }

    public Result Update(
    string companyName,
    string tin,
    string email,
    string phone,
    string slogan,
    string street,
    string houseNumber,
    string postalCity,
    string postalCode,
    string city,
    string country,
    string? flatNumber,
    IAddressValidationService validator)
    {
        var validationResult = ValidateBasicInfo(companyName, tin, email, phone, slogan);
        if (validationResult.IsFailure)
            return Result.Failure<Company>(validationResult.Error);

        var addressResult = AddressValueObject.Create(street, houseNumber, postalCity, postalCode, city, country, flatNumber, validator);
        if (addressResult.IsFailure)
            return Result.Failure<Company>(addressResult.Error);

        CompanyName = companyName;
        TIN = tin;
        Email = email;
        Phone = phone;
        Slogan = slogan;
        CompanyAddress = addressResult.Value;
        DateEdited = DateTime.UtcNow;

        return Result.Success();
    }
}
