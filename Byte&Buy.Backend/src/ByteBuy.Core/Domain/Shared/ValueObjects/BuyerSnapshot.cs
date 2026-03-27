using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Orders.Errors;
using ByteBuy.Core.Domain.Shared.ResultTypes;

namespace ByteBuy.Core.Domain.Shared.ValueObjects;

public class BuyerSnapshot : ValueObject
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }
    public AddressValueObject Address { get; private set; } = null!;

    private BuyerSnapshot() { }

    private BuyerSnapshot(
        string fullName,
        string email,
        string? phoneNumber,
        AddressValueObject address)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public static Result<BuyerSnapshot> Create(string firstName,
        string lastName,
        string? phoneNumber,
        string email,
        AddressValueObject? address)
    {

        if (address is null)
            return Result.Failure<BuyerSnapshot>(BuyerSnapshotErrors.HomeAddressNotSet);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            return Result.Failure<BuyerSnapshot>(BuyerSnapshotErrors.PhoneNumberNotSet);

        var fullName = $"{firstName} {lastName}";

        return new BuyerSnapshot(fullName, email, phoneNumber, address.Copy());
    }

    public BuyerSnapshot Copy()
    {
        return new BuyerSnapshot(FullName, Email, PhoneNumber, Address.Copy());
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FullName;
        yield return Email;
        yield return PhoneNumber;
        yield return Address;
    }
}