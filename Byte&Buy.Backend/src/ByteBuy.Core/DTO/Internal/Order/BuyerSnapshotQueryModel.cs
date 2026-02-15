using ByteBuy.Core.DTO.Public.AddressValueObj;

namespace ByteBuy.Core.DTO.Internal.Order;

public sealed record BuyerSnapshotQueryModel(
    string FullName,
    string Email,
    string PhoneNumber,
    HomeAddressDto Address
);