using ByteBuy.Core.Domain.Shared.Enums;

namespace ByteBuy.Core.DTO.Internal.Seller;

public record SellerSnapshotQueryModel(
    Guid SellerId,
    SellerType Type,
    string DisplayName,
    string? TIN,
    AddressValueObject Address);