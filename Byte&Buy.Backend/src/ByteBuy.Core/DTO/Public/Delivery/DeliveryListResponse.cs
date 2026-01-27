namespace ByteBuy.Core.DTO.Public.Delivery;

public record DeliveryListResponse(
    Guid Id,
    string Name,
    string Currency,
    decimal Amount,
    string Carrier
    );
