using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.Seller;

public record SellerSnapshotDto(
    Guid SellerId,
    SellerType Type,
    string DisplayName,
    string? TIN,
    AddressValueObject Address);