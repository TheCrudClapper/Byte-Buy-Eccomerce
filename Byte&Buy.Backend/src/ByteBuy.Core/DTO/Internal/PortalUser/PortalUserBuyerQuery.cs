using ByteBuy.Core.Domain.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.PortalUser;

/// <summary>
/// Internal dto used to get user data while creating order
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Email"></param>
/// <param name="Phone"></param>
/// <param name="Address"></param>
public record PortalUserBuyerQuery(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    AddressValueObject Address);