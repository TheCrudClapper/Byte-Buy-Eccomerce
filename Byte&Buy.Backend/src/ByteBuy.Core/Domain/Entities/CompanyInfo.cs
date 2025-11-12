using ByteBuy.Core.Domain.EntityContracts;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.Entities;

/// <summary>
/// Represents unique information about the ByteBuy company.
/// This entity cannot be deleted and only one instance exists in the system.
/// </summary>
public class CompanyInfo : AuditableEntity
{
    public string CompanyName { get; private set; } = null!;
    public string TIN { get; private set; } = null!;
    public AddressValueObj Address { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string? Slogan { get; private set; }

    private CompanyInfo() { }

    private CompanyInfo(
        string companyName,
        string tin,
        string email,
        string phone,
        string? slogan,
        AddressValueObj address)
    {
        Address = address;
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
       string? slogan)
    {
        if (string.IsNullOrWhiteSpace(companyName) || companyName.Length > 50)
            return Result.Failure(Error.Validation("Company name is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(tin) || tin.Length > 20)
            return Result.Failure(Error.Validation("TIN is required and must be at most 20 characters."));

        if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
            return Result.Failure(Error.Validation("Email is required and must be at most 50 characters."));

        if (string.IsNullOrWhiteSpace(phone) || phone.Length > 16)
            return Result.Failure(Error.Validation("Phone is required and must be at most 16 characters."));

        if (!string.IsNullOrWhiteSpace(slogan) && slogan.Length > 30)
            return Result.Failure(Error.Validation("Slogan must be at most 30 characters."));

        return Result.Success();
    }

    public static Result<CompanyInfo> Create(
    string companyName,
    string tin,
    string email,
    string phone,
    string? slogan,
    string street,
    string houseNumber,
    string postalCode,
    string city,
    string country,
    string? flatNumber)
    {
        var validationResult = ValidateBasicInfo(companyName, tin, email, phone, slogan);
        if (validationResult.IsFailure)
            return Result.Failure<CompanyInfo>(validationResult.Error);

        var addressResult = AddressValueObj.Create(street, houseNumber, postalCode, city, country, flatNumber);
        if (addressResult.IsFailure)
            return Result.Failure<CompanyInfo>(addressResult.Error);

        return new CompanyInfo(companyName, tin, email, phone, slogan, addressResult.Value);
    }

    public Result Update(
    string companyName,
    string tin,
    string email,
    string phone,
    string? slogan,
    string street,
    string houseNumber,
    string postalCode,
    string city,
    string country,
    string? flatNumber)
    {
        var validationResult = ValidateBasicInfo(companyName, tin, email, phone, slogan);
        if (validationResult.IsFailure)
            return Result.Failure<CompanyInfo>(validationResult.Error);

        var addressResult = AddressValueObj.Create(street, houseNumber, postalCode, city, country, flatNumber);
        if (addressResult.IsFailure)
            return Result.Failure<CompanyInfo>(addressResult.Error);

        CompanyName = companyName;
        TIN = tin;
        Email = email;
        Phone = phone;
        Slogan = slogan;
        Address = addressResult.Value;

        return Result.Success();
    }
}
