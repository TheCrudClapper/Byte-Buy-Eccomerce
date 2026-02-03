using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.PortalUser;

public record PortalUserBuyerQuery(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    AddressValueObject Address);