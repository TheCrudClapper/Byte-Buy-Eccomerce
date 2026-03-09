using ByteBuy.Core.Domain.Shared.Enums;
using ByteBuy.Core.Domain.Shared.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.Seller;

public record SellerSnapshotQueryModel(
    Guid SellerId,
    SellerType Type,
    string DisplayName,
    string? TIN,
    AddressValueObject Address);