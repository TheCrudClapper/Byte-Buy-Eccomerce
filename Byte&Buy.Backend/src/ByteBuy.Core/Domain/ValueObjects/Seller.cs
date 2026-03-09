using ByteBuy.Core.Domain.Base;
using ByteBuy.Core.Domain.Enums;

namespace ByteBuy.Core.Domain.ValueObjects;

// Represents a business representation of a seller based of Seller Type.
public class Seller : ValueObject
{
    public SellerType Type { get; init; }

    // Id is evaluated based on type property
    public Guid Id { get; init; }

    private Seller(SellerType type, Guid id)
    {
        Id = id;
        Type = type;
    }

    public static Seller CreatePrivateSeller(Guid userId)
        => new Seller(SellerType.PrivatePerson, userId);

    public static Seller CreateCompanySeller(Guid companyId)
        => new Seller(SellerType.Company, companyId);

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return Id;
    }
}
