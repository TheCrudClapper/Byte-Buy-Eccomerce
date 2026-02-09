using ByteBuy.Core.ResultTypes;

namespace ByteBuy.Core.Domain.ValueObjects;

public class BuyerSnapshot
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

        return new BuyerSnapshot(fullName, email, phoneNumber, address);
    }
}