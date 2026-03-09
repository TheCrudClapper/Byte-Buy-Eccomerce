using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Shared.Enums;
namespace ByteBuy.Core.Domain.Shared.ValueObjects;

public class SellerSnapshot : ValueObject
{
    public SellerType Type { get; private set; }
    public Guid SellerId { get; private set; }
    public string DisplayName { get; private set; } = null!;
    public string? TIN { get; private set; }
    public AddressValueObject Address { get; private set; } = null!;

    private SellerSnapshot() { }

    private SellerSnapshot(SellerType type, Guid sellerId, string displayName, string? tIN, AddressValueObject address)
    {
        Type = type;
        SellerId = sellerId;
        DisplayName = displayName;
        TIN = tIN;
        Address = address;
    }

    public static SellerSnapshot CreateCompanySnapshot(Guid sellerId, string displayName, string? tIN, AddressValueObject address)
        => new(SellerType.Company, sellerId, displayName, tIN, address);

    public static SellerSnapshot CreatePrivateSellerSnapshot(Guid sellerId, string displayName, string? tIN, AddressValueObject address)
        => new(SellerType.PrivatePerson, sellerId, displayName, tIN, address);

    public SellerSnapshot Copy()
    {
        var addressCopy = Address.Copy();
        return new SellerSnapshot(Type, SellerId, DisplayName, TIN, addressCopy);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return SellerId;
        yield return DisplayName;
        yield return TIN;
        yield return Address;
    }
}