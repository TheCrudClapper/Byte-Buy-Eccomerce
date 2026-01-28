namespace ByteBuy.Core.DTO.Public.Order;

public record OrderAddRequest(
    Guid PaymentMethodId,
    IEnumerable<SellerDeliveryRequest> SelectedDeliveries);

