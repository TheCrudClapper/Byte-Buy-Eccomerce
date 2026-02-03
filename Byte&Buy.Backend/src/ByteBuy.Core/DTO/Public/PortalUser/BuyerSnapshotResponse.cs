using ByteBuy.Core.DTO.Public.AddressValueObj;

namespace ByteBuy.Core.DTO.Public.PortalUser;

public sealed record BuyerSnapshotResponse(
     string FullName,
     string Email,
     string PhoneNumber,
     HomeAddressDto Address);
