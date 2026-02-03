using ByteBuy.Core.Domain.Entities;

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

    public static BuyerSnapshot Create(string firstName,
        string lastName,
        string phoneNumber,
        string email,
        AddressValueObject address)
        => new(
            $"{firstName} {lastName}",
            email!,
            phoneNumber,
            address);


}