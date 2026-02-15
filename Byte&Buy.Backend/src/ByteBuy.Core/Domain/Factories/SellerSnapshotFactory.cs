using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;
using ByteBuy.Core.DTO.Internal.Seller;

namespace ByteBuy.Core.Domain.Factories;

public static class SellerSnapshotFactory
{
    public static SellerSnapshot CreateSnapshot(SellerSnapshotQueryModel dto)
    {
        return dto.Type switch
        {
            SellerType.Company =>
                SellerSnapshot.CreateCompanySnapshot(
                    dto.SellerId,
                    dto.DisplayName,
                    dto.TIN,
                    dto.Address),

            SellerType.PrivatePerson =>
                SellerSnapshot.CreatePrivateSellerSnapshot(
                    dto.SellerId,
                    dto.DisplayName,
                    null,
                    dto.Address
                    ),

            _ => throw new ArgumentOutOfRangeException(nameof(dto), $"Unsupported seller type or dto is null"),
        };
    }
}
