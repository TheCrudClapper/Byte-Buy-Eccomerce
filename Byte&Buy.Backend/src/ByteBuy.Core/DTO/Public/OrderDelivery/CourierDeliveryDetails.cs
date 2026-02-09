namespace ByteBuy.Core.DTO.Public.OrderDelivery;

public sealed record CourierDeliveryDetails(
    string Street,
    string HouseNumber,
    string? FlatNumber,
    string City,
    string PostalCity,
    string PostalCode) : OrderDeliveryDetails;