using ByteBuy.Core.Domain.Shared.ValueObjects;

namespace ByteBuy.Core.DTO.Internal.PortalUser;

/// <summary>
/// Internal dto used to get user data while creating order
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Email"></param>
/// <param name="Phone"></param>
/// <param name="Address"></param>
public record PortalUserBuyerQueryModel(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    AddressValueObject? Address);