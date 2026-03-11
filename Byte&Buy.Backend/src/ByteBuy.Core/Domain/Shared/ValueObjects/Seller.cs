using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Shared.Enums;

namespace ByteBuy.Core.Domain.Shared.ValueObjects;

// Represents a business representation of a seller based of Seller Type.
public class Seller : ValueObject
{
    public SellerType Type { get; init; }

    // SellerId is evaluated based on type property
    public Guid SellerId { get; init; }

    private Seller() { }
    private Seller(SellerType type, Guid id)
    {
        SellerId = id;
        Type = type;
    }

    public static Seller CreatePrivateSeller(Guid userId)
        => new Seller(SellerType.PrivatePerson, userId);

    public static Seller CreateCompanySeller(Guid companyId)
        => new Seller(SellerType.Company, companyId);

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return SellerId;
    }
}
