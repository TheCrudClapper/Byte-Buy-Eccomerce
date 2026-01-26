using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.Domain.ValueObjects;

public class SellerSnapshot
{
    public SellerType Type { get; private set; }
    public Guid SellerId { get; private set; }
    public string DisplayName { get; private set; } = null!;
    public string? TIN { get; private set; }
    public AddressValueObject Address { get; private set; } = null!;

    private SellerSnapshot() { }

}